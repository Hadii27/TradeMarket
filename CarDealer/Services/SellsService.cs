using CarDealer.Data;
using CarDealer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TradeMarket.Models;

namespace TradeMarket.Services
{
    public class SellsService : ISellsService
    {
        private readonly DataContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IGlobalMethodsService _globalMethods;

        public SellsService(DataContext context , UserManager<ApplicationUser> userManager, IGlobalMethodsService globalMethods)
        {
            _context = context;
            _userManager = userManager;
            _globalMethods = globalMethods;
        }

        public async Task<IEnumerable<CategoryModel>> Category()
        {
            var categories = await _context.categories.ToListAsync();
            return categories;
        }

        public async Task<IEnumerable<SubCategories>> SubCategory(int CatID)
        {
            var Subs = await _context.subCategories.ToListAsync();
            return Subs;
        }

        public async Task<string> Sell(int categoryid ,SellModel model, int SubCategory)
        {
            var userId = _globalMethods.CurrentUser();

            var user = await _context.Users
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();

            var UserCountry = user.NationalityId;
            var UserCity = user.CityId;

            var country = await _context.countries
                .Where(c => c.Id == UserCountry)
                .FirstOrDefaultAsync();

            var city = await _context.cities
                 .Where(c => c.Id == UserCity)
                 .FirstOrDefaultAsync();

            var category = await _context.categories.FindAsync(categoryid);
            if (category == null)           
                return "Invalid cat id";

            var sub = await _context.subCategories.FindAsync(SubCategory);
            if (sub == null) 
                return "You must choose the sub category";

            var categoryName = await _context.categories
                .Where(c => c.Id == model.CateId)
                .Select(c => c.Name)
                .FirstOrDefaultAsync();

            var sell = new SellModel
            {
                name = model.name,
                description = model.description,
                CateId = categoryid,
                SubCatId = SubCategory,
                CatName = category.Name,
                PurchaseDate = model.PurchaseDate,
                price = model.price,
                priceType = model.priceType,
                quantity = model.quantity,
                CountryId = UserCountry,
                CityId = UserCity,
                CityName = city.Name,
                CountryName = country.Name,
                Condition = model.Condition,
                PostTime = DateTime.Now,

                UserId = userId,
                PhoneNumber = model.PhoneNumber,
                Username = user.UserName,
            };

            var result = await _context.sells.AddAsync(sell);
            _context.SaveChanges();
            return "Done";
        }

    }
}
