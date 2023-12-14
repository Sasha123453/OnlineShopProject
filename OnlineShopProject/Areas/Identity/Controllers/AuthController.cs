using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;
using OnlineShopProject.Areas.Identity.Interfaces;
using OnlineShopProject.Areas.Identity.Models;
using OnlineShopProject.Controllers;

namespace OnlineShopProject.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        public AuthController(SignInManager<User> signInManager, UserManager<User> userManager, IAuthService authService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _authService = authService;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.SignInUser(login);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(ShopController.ProductsPage));
                }
                ModelState.AddModelError("", "Неверный пароль или логин");
            }
            return View("Login");
        }
        [HttpPost]
        public async Task<IActionResult> Register(Register register)
        {
            if (ModelState.IsValid)
            {
                var res = await _authService.RegisterUser(register);
                if (res.Succeeded)
                {
                    return RedirectToAction("ProductsPage","Shop");
                }
                foreach (var error in res.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
            }
            return View("Register");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        public async Task<IActionResult> LogOut()
        {
            await _authService.LogOut();
            return RedirectToAction(nameof(ShopController.ProductsPage));
        }
    }
}
