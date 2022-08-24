using System.ComponentModel.DataAnnotations;

namespace UserMessengerService.Domain.Dto
{
    public class RegistrationDto
    {
        [Required]
        [StringLength(30, MinimumLength = 6)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string EMail { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string Role { get; set; } = "user";
    }
}
