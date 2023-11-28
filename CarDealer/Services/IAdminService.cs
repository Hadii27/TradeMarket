using TradeMarket.Models;

namespace TradeMarket.Services
{
    public interface IAdminService
    {
        public Task<IEnumerable<CategoryModel>> GetCategories();
        public Task<CountryModel> CreateCountry(CountryModel model);
        public Task<string> CreateCity(List<CityModel> model, int CountryID);
        public Task<CategoryModel> CreateCategory(CategoryModel category);
        public Task<string> CreateSubCategory(SubCategories Sub, int catID);
    }
}
