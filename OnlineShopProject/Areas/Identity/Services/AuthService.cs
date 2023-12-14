using Microsoft.AspNetCore.Identity;
using OnlineShop.Db.Models;
using OnlineShopProject.Areas.Identity.Interfaces;
using OnlineShopProject.Areas.Identity.Models;

namespace OnlineShopProject.Areas.Identity.Services
{
    public class AuthService : IAuthService
    {
        public UserManager<User> _userManager;
        public SignInManager<User> _signInManager;
        public RoleManager<IdentityRole> _roleManager;
        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public async Task<SignInResult> SignInUser(Login login)
        {
            var res = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, login.RememberMe, false);
            return res;
        }
        public async Task<IdentityResult> RegisterUser(Register register)
        {
            User user = new User { UserName = register.UserName, Email = register.Email };
            var res = await _userManager.CreateAsync(user, register.Password);
            if (res.Succeeded)
            {
                _userManager.AddToRoleAsync(user, Constants.DefaultUserRole);
            }
            return res;
        }
        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
