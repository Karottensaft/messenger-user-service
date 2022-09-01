using System.ComponentModel.DataAnnotations;

namespace UserMessengerService.Domain.Dto;

public class ChangingDto
{
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
}