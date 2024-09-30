using System.ComponentModel.DataAnnotations;

namespace BlazorAccountManagement.Models
{
    public class UserProfile
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }
    }
}
