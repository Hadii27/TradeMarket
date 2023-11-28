using TradeMarket.Models;

namespace TradeMarket.Services
{
    public interface IUsersService
    {
        public Task<IEnumerable<FavouriteDetailsModel>> GetFavouriteCollection();
        public string Delete(int PostID);

    }
}
