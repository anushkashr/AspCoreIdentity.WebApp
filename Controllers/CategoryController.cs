using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspCoreIdentity.WebApp.Controllers
{

    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
