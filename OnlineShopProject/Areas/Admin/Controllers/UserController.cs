using Microsoft.AspNetCore.Mvc;

namespace OnlineShopProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        public IActionResult Users()
        {
            return View();
        }
    }
}
