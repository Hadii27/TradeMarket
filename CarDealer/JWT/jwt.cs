namespace CarDealer.JWT
{
    public class jwt
    {

        public string key { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;

        public string Audience { get; set; } = string.Empty;

        public double DurationInHours { get; set; }

    }
}
