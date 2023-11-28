using CarDealer.Data;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using TradeMarket.Dtos;
using TradeMarket.Models;

namespace TradeMarket.Services
{
    public class AdminService : IAdminService
    {
        private readonly DataContext _context;
        private readonly IGlobalMethodsService _globalMethodsService;
        public AdminService (DataContext context, IGlobalMethodsService globalMethodsService)
        {
            _context = context;
            _globalMethodsService = globalMethodsService;
        }

        public async Task<IEnumerable<CategoryModel>> GetCategories()
        {
            var categories = await _context.categories
                .ToListAsync();
            return categories;
        }

        public async Task<CountryModel> CreateCountry(CountryModel model)
        {
            model.Name = _globalMethodsService.CapitalizeFirstLetter(model.Name);
            var existingCountry = await _context.countries
                .Where(c => c.Name == model.Name)
                .FirstOrDefaultAsync();

            if (existingCountry != null)           
                return null;
            
            await _context.countries.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<string> CreateCity(List<CityModel> model,int CountryID)
        {
            var createdCityNames = new List<string>();

            foreach (var city in model)
            {
                    city.Name = _globalMethodsService.CapitalizeFirstLetter(city.Name);
                    var country = await _context.countries
                        .Where(c => c.Id == city.countryId)
                        .FirstOrDefaultAsync();
                    if (country is null)
                        return "Invalid country";

                    var ExistCity = await _context.cities
                        .Where(c => c.Name == city.Name)
                        .FirstOrDefaultAsync();

                    if (ExistCity is not null)
                        return "this city already exist";

                    var newCity = new CityModel
                    {
                        Name = city.Name,
                        countryId = CountryID
                    };

                    createdCityNames.Add(newCity.Name);
                    await _context.cities.AddAsync(newCity);                
            }

            _context.SaveChanges();
            var createdCitiesString = string.Join(", ", createdCityNames);
            return $"Done, you create {createdCitiesString}";
        }

        public async Task<CategoryModel> CreateCategory(CategoryModel category)
        {
            category.Name = _globalMethodsService.CapitalizeFirstLetter(category.Name);

            var name = await _context.categories
                .Where(n => n.Name == category.Name)
                .FirstOrDefaultAsync();

            if (name is not null)           
                return null;

            await _context.categories.AddAsync(category);
            _context.SaveChanges();
            return category;                        
        }

        public async Task<string> CreateSubCategory(SubCategories Sub, int catID)
        {
            Sub.Name = _globalMethodsService.CapitalizeFirstLetter(Sub.Name);
            var category = await _context.categories.FindAsync(catID);

            if (category is null)
                return "You must choose a correct category";

            var name = await _context.subCategories
                .Where(n => n.Name == Sub.Name && n.CategoryId == catID)
                .FirstOrDefaultAsync();
            if (name is not null)
                return "This sub Category Already exist!";

            var sub = new SubCategories
            {
                Name = Sub.Name,
                CategoryId = catID,
                CategoryName = category.Name,
            };

            var result = await _context.subCategories.AddAsync(sub);
             _context.SaveChanges();
            return $"Done you create {sub.Name} ";
        }



    }
}
