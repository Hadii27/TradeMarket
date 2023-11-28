namespace TradeMarket.Models
{
    public class SellModel
    {
        public int id { get; set; }
        public int CateId { get; set; }
        public int SubCatId { get; set; }
        public string CatName { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string price { get; set; }
        public string priceType { get; set; }
        public int quantity { get; set; }
        public DateTime PostTime { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string PhoneNumber { get; set; }
        public string Condition { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }


    }
}
