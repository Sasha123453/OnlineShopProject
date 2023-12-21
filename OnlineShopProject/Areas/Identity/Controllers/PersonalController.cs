using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;
using OnlineShopProject.Areas.Identity.Services;
using System.Data.OleDb;

namespace OnlineShopProject.Areas.Identity.Controllers
{
    [Area("Identity")]
    [Authorize]
    public class PersonalController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IEmailSender<User> _identityEmailSender;
        public PersonalController(UserManager<User> userManager, IEmailSender<User> identityEmailSender)
        {
            _userManager = userManager;
            _identityEmailSender = identityEmailSender;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> IsEmailConfirmed()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            return Json(new { confirmed = await _userManager.IsEmailConfirmedAsync(user) });
        } 
    }
}
