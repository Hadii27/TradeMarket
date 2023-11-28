using System.Text.Json.Serialization;

namespace TradeMarket.Models
{
    public class SubCategories
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public CategoryModel Category { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
