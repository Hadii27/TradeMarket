using CarDealer.Models;
using System.IdentityModel.Tokens.Jwt;
using TradeMarket.Models;

namespace TradeMarket.Services
{
    public interface IAuthService
    {
        public Task<AuthModel> Register(RegisterModel model);
        public Task<JwtSecurityToken> CreateJwtTokenAsync(ApplicationUser user);
        public Task<AuthModel> GetToken(TokenRequestModel model);

        public Task<IEnumerable<CountryModel>> Countries();
        public Task<IEnumerable<CityModel>> Cities(int CountryId);

    }
}
