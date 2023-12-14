using Microsoft.AspNetCore.Mvc;

namespace OnlineShopProject.Areas.Admin.Controllers
{
    public class RoleController : Controller
    {
        public IActionResult Roles()
        {
            return View();
        }
    }
}
