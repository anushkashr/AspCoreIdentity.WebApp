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

		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
		
		public string? Address { get; set; }

		public IFormFile Photo { get; set; }

	}
}
