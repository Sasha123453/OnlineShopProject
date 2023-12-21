using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopProject.Areas.Identity.Models;
using OnlineShopProject.Controllers;
using OnlineShopProject.Interfaces;
using System.Data.Common;
using System.Diagnostics;
using System.Net.Mail;

namespace OnlineShopProject.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class AuthController : Controller
    {
        private readonly ISessionsRepository<Cart> _sessionCartsRepository;
        private readonly IEmailSender<User> _emailSender;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ICartsRepository _cartsRepository;
        private readonly ILogger<AuthController> _logger;
        public AuthController(SignInManager<User> signInManager, UserManager<User> userManager, IEmailSender<User> emailSender, ISessionsRepository<Cart> sessionCartsRepository, ICartsRepository cartsRepository, ILogger<AuthController> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _emailSender = emailSender;
            _sessionCartsRepository = sessionCartsRepository;
            _cartsRepository = cartsRepository;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login login, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = login.UserName };
                var result = await _signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(login.ReturnUrl))
                    {
                        return Redirect(login.ReturnUrl);
                    }
                    return RedirectToAction("ProductsPage", "Shop");
                }
                ModelState.AddModelError("", "Неверный пароль или логин");
            }
            return View("Login");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Register register)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    Email = register.Email,
                    UserName = register.UserName,
                };
                var res = await _userManager.CreateAsync(user, register.Password);
                if (res.Succeeded)
                {
                    try
                    {
                        _logger.LogInformation($"Пользователь с почтой {user.Email} успешно зарегистрировался");
                        var registeredUser = await _userManager.FindByEmailAsync(user.Email);
                        var cart = _sessionCartsRepository.Get();
                        if (cart != null) cart.UserId = registeredUser.Id;
                        await _cartsRepository.AddCartAsync(cart?.Items, registeredUser.Id);
                        _sessionCartsRepository.Clear();
                        await _userManager.AddToRoleAsync(registeredUser, Constants.DefaultUserRole);
                        await _signInManager.SignInAsync(registeredUser, true);
                        await SendEmail(registeredUser, registeredUser.Email);
                    }
                    catch (SmtpException ex)
                    {
                        _logger.LogError($"При отправке кода подтверждения на почту произошла ошибка {ex.Message}" +
                            $"/n метод : {ex.TargetSite} " +
                            $"/n источник ошибки : {ex.Source}");
                        throw;
                    }
                    catch (DbException ex)
                    {
                        _logger.LogError($"");
                        throw;
                    }
                    if (!string.IsNullOrEmpty(register.ReturnUrl))
                    {
                        return Redirect(register.ReturnUrl);
                    }
                    return RedirectToAction("ProductsPage", "Shop");
                }
                foreach (var error in res.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
            }
            return View("Register");
        }
        [NonAction]
        public async Task SendEmail(User user, string email)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action("ConfirmEmail", "Auth", new { token, email }, Request.Scheme);
            await _emailSender.SendConfirmationLinkAsync(user, email, confirmationLink);
        }
        [Authorize]
        public async Task<IActionResult> SendEmail()
        {
            var user = await _userManager.GetUserAsync(User);
            await SendEmail(user, user.Email);
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var res = await _userManager.ConfirmEmailAsync(user, token);
            return View(res.Succeeded);
        }
        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            return View(new Register { ReturnUrl = returnUrl});
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("ProductsPage", "Shop");
        }
    }
}
