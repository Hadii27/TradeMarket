using TradeMarket.Models;

namespace TradeMarket.Services
{
    public interface ISellsService
    {
        public Task<string> Sell(int categoryid, SellModel model, int SubCategory);
        public Task<IEnumerable<CategoryModel>> Category();
        public Task<IEnumerable<SubCategories>> SubCategory(int CatID);


    }
}
