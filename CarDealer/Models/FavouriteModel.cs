
namespace TradeMarket.Models
{
    public class FavouriteModel
    {
        public int id { get; set; }

        public string UserID { get; set; }

        public List<FavouriteDetailsModel> favouriteDetails { get; set; }

        public int Count { get; set; }
    }
}
