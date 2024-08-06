
using System.ComponentModel.DataAnnotations;
namespace ProjectCarRental.Models
{
    public class AspNetUsers
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
    }
}
