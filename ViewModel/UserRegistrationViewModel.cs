using System.ComponentModel.DataAnnotations;

namespace AspCoreIdentity.WebApp.ViewModel
{
    public class UserRegistrationViewModel
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage ="Password and Confirm Password do not match!")]
        public string ConfirmPassword { get; set; }
        
    }
}
