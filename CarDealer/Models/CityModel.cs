using System.ComponentModel.DataAnnotations;

namespace TradeMarket.Models
{
    public class CityModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int countryId { get; set; }
        public CountryModel country { get; set; }
    }
}
