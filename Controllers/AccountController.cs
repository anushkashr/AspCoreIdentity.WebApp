using AspCoreIdentity.WebApp.ViewModel;
using Identity.Models.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspCoreIdentity.WebApp.Controllers
{
	[AllowAnonymous]
	public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        //to reach to root url
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, 
            IWebHostEnvironment webHostEnvironment)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }
     
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
            if(ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if(result.Succeeded)
                {
                    return RedirectToAction("index", "Home");
                }
                ModelState.AddModelError("", "Invalid email or password provided");
            }
			return View(model);
		}
		public IActionResult Register()
        {
            return View();
        }

        private string UploadPhoto(IFormFile file)
        {
            if (file==null)
            {
                return string.Empty;
            }
            string uploadToFolder = Path.Combine(_webHostEnvironment.WebRootPath, "profile_img");
            //generate random/unique id for uploaded photo
            string fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            
            //full file path
            string fullFilePath = Path.Combine(uploadToFolder,fileName);
            using (var fileStream = new FileStream(fullFilePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
            return fileName;
        }

        [HttpPost]
		public async Task<IActionResult> Register(UserRegistrationViewModel model)
		{
            //ModelState.Remove("Photo");
            if (ModelState.IsValid)
            {
                //1. Upload Photo
                string userImg = UploadPhoto(model.Photo);

                var user = new ApplicationUser()
                {
                    Email = model.Email,
                    UserName = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = model.Address,
                    Photo = userImg
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if(result.Succeeded)
                {
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Code+ "" + error.Description);
                    }
                }
            }
				return View(model);
		}

		public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

    }
}
