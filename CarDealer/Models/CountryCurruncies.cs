namespace TradeMarket.Models
{
    public class CountryCurruncies
    {
        public int CountryId { get; set; }
        public CountryModel country { get; set; }

        public int CurrencyId { get; set; }

        public CurrencyModel currency { get; set; }

    }
}
