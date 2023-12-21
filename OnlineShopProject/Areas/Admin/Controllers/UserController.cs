using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShopProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Constants.DefaultAdminRole)]
    public class UserController : Controller
    {
        public IActionResult Users()
        {
            return View();
        }
    }
}
