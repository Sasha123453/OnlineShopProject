using Microsoft.AspNetCore.Mvc;

namespace OnlineShopProject.Controllers
{
    public class IdentityController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Registration()
        {
            return View();
        }
    }
}
