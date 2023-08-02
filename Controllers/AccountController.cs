using AspCoreIdentity.WebApp.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspCoreIdentity.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
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
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
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

    }
}
