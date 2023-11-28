using System.ComponentModel.DataAnnotations;

namespace TradeMarket.Models
{
    public class RegisterModel
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public int NationalityId { get; set; }

        [Required]
        public int CityId { get; set; }



    }
}
