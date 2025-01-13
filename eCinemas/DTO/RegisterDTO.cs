using System.ComponentModel.DataAnnotations;

namespace eCinemas.DTO
{
    public class RegisterDTO
    {
        [Display(Name = "Full name")]
        [Required(ErrorMessage = "Full name is required")]
        public string? FullName { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is required")]
        public string? EmailAddress { get; set; }

        [Required]
        [RegularExpression(@"(?=.*?[A-Z])(?=.*?[a-z])(?=.*?\d)(?=.*?\W)^.{8,}$",
            ErrorMessage = "Password must contain at least 8, one uppercase, one lowercase, " +
            "one digit, and one special character")]
        [DataType(DataType.Password)]        
        public string? Password { get; set; }

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string? ConfirmPassword { get; set; }
    }
}
