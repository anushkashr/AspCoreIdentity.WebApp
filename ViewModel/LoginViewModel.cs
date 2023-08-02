using System.ComponentModel.DataAnnotations;

namespace AspCoreIdentity.WebApp.ViewModel
{
    public class LoginViewModel
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }    
    }
}
