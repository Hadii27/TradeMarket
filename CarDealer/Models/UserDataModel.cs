namespace TradeMarket.Models
{
    public class UserDataModel
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int CountryID { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int CityID { get; set; }

        public List<string> Roles { get; set; }
    }
}
