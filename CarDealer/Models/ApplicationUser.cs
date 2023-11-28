using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CarDealer.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int NationalityId { get; set; }

        public int CityId { get; set; }

        public string Nationalality { get; set; }

        public string City { get; set; }


    }
}
