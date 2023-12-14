using Microsoft.AspNetCore.Identity;
using OnlineShop.Db.Models;
using OnlineShopProject.Areas.Identity.Models;

namespace OnlineShopProject.Areas.Identity.Interfaces
{
    public interface IAuthService
    {
        public Task<SignInResult> SignInUser(Login login);
        public Task<IdentityResult> RegisterUser(Register register);
        public Task LogOut();
    }
}
