using System.ComponentModel.DataAnnotations;

namespace Reservations.Models
{
    public class NewPersonViewModel
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "First name is required")]
        public string FirstName { get; set; } = string.Empty;

        public string MiddleName { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Last name is required")]
        public string LastName { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Email name is required")]
        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;
    }
}
