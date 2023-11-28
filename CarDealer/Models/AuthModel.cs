namespace TradeMarket.Models
{
    public class AuthModel
    {
        public string Message { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public DateTime ExpireOn { get; set; }
        public bool isAuthenticated { get; set; }

        public UserDataModel UserData { get; set; }
    }
}
