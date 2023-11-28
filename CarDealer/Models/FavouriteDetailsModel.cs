using System.Text.Json.Serialization;

namespace TradeMarket.Models
{
    public class FavouriteDetailsModel
    {
        public int Id { get; set; }
        [JsonIgnore]
        public FavouriteModel Favourite { get; set; }
        public int FavouriteID { get; set; }

        public SellModel sell { get; set; }
        public int sellId { get; set; }

    }
}