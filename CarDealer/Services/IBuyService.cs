using TradeMarket.Models;

namespace TradeMarket.Services
{
    public interface IBuyService
    {
        public Task<IEnumerable<CategoryModel>> Category();
        public Task<IEnumerable<CategoryModel>> CategoryByID(int CatId);
        public Task<IEnumerable<SubCategories>> SubCategory(int CatId);
        public Task<IEnumerable<SellModel>> GetPosts(int CatId, int SubID);
        public Task<IEnumerable<SellModel>> GetPostsOfall(int CatId);
        public Task<string> addFavourite(int PostID);


    }
}
