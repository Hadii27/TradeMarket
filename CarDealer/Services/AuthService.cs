using CarDealer.Data;
using CarDealer.JWT;
using CarDealer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TradeMarket.Models;

namespace TradeMarket.Services
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly jwt _jwt;
        public AuthService (DataContext context, UserManager<ApplicationUser> userManager, IOptions<jwt> jwt)
        {
            _context = context;
            _userManager = userManager;
            _jwt = jwt.Value;
        }

        public async Task<AuthModel> Register(RegisterModel model)
        {
            var National = await _context.countries.FindAsync(model.NationalityId);
            var cityId = await _context.cities.FindAsync(model.CityId);

            var nationalName = await _context.countries
                .Where(n => n.Id == model.NationalityId)
                .FirstOrDefaultAsync();

            var City = await _context.cities
                .Where(c => c.Id == model.CityId)
                .FirstOrDefaultAsync();

            if (National == null || nationalName == null)           
                return new AuthModel { Message = "Invalid Country"};

            if (cityId == null || City == null)
                return new AuthModel { Message = "Invalid City" };


            var username = await _userManager.FindByEmailAsync(model.Username);
            var Email = await _userManager.FindByEmailAsync(model.Email);
            if (username is not null || Email is not null)            
                return new AuthModel { Message = "Username or Email is already exist" };

            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                FirstName = model.FirstName,
                LastName = model.LastName,
                NationalityId = model.NationalityId,
                Nationalality = nationalName.Name,
                CityId = model.CityId,
                City = City.Name,
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                string errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description}, ";
                }
                return new AuthModel { Message = errors };
            }

            var CreateJwtToken = await CreateJwtTokenAsync(user);

            return new AuthModel
            {

                ExpireOn = CreateJwtToken.ValidTo,
                isAuthenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(CreateJwtToken),
                UserData = new UserDataModel
                {
                    Username = model.Username,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    UserId = user.Id,
                    Roles = new List<string> { "" },
                    City = City.Name,
                    Country = nationalName.Name,
                }
            };
        }
        public async Task<AuthModel> GetToken(TokenRequestModel model)
        {
            var authModel = new AuthModel();
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authModel.Message = "Email Or Password Is invalid";
                return authModel;
            }

            var jwtSecurityToken = await CreateJwtTokenAsync(user);
            var roleList = await _userManager.GetRolesAsync(user);
            var userData = new UserDataModel
            {
                UserId = user.Id,
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Roles = roleList.ToList()

            };
            authModel.UserData = userData;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.ExpireOn = jwtSecurityToken.ValidTo;
            authModel.isAuthenticated = true;

            return authModel;
        }
        public async Task<JwtSecurityToken> CreateJwtTokenAsync(ApplicationUser user)
        {
            var userclaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles", role));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("UserId", user.Id.ToString()),
            }
            .Union(userclaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var JwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddHours(_jwt.DurationInHours),
                signingCredentials: signingCredentials
                );
            return JwtSecurityToken;
        }

        public async Task<IEnumerable<CountryModel>> Countries()
        {
            var countries = await _context.countries
                .OrderBy(n => n.Name)
                .ToListAsync();
            return countries;
        }

        public async Task<IEnumerable<CityModel>> Cities(int CountryId)
        {
            var city = await _context.cities
                .OrderBy(c => c.Name)
                .Where(c => c.countryId == CountryId)
                .ToListAsync();
            return city;
        }
    }
}
