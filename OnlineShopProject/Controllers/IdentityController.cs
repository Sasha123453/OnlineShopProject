using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;
using OnlineShopProject.Models;
using Serilog;
using System.Security.Cryptography;

namespace OnlineShopProject.Controllers
{
    public class IdentityController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public IdentityController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            string hashedPassword = HashPassword(login.Password);
            if (ModelState.IsValid)
            {
                var res = await _signInManager.PasswordSignInAsync(login.UserName, hashedPassword, login.RememberMe, false);
                if (res.Succeeded)
                {
                    ModelState.AddModelError("", "Неверный пароль или логин");
                    return RedirectToAction("Identity", "Login");
                }
            }
            return RedirectToAction("ProductsPage", "Shop");
        }
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Registration(RegistrationViewModel registration)
        {
            return View();
        }
        [NonAction]
        public string HashPassword(string password)
        {
            MD5 hash = MD5.Create();
            var bytes = System.Text.Encoding.ASCII.GetBytes(password);
            return Convert.ToHexString(hash.ComputeHash(bytes));
        }
    }
}
