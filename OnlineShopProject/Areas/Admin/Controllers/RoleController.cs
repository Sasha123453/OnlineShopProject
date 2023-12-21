using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShopProject.Areas.Admin.Controllers
{
    [Authorize(Roles = Constants.DefaultAdminRole)]
    public class RoleController : Controller
    {
        public IActionResult Roles()
        {
            return View();
        }
    }
}
