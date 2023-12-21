using Microsoft.AspNetCore.Identity;
using OnlineShop.Db;
using OnlineShop.Db.Models;

namespace OnlineShopProject
{
    public class IdentityInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminName = "admin";
            string adminLogin = "admin@mail.com";
            string password = "Passwd123$";
            var role = await roleManager.FindByNameAsync(Constants.DefaultUserRole);
            if (!await roleManager.RoleExistsAsync(Constants.DefaultAdminRole))
            {
                await roleManager.CreateAsync(new IdentityRole(Constants.DefaultAdminRole));
            }
            if (await userManager.FindByNameAsync(adminLogin) is null)
            {
                User admin = new User { Email = adminLogin, UserName = adminName };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, Constants.DefaultAdminRole);
                }
            }
            if (!await roleManager.RoleExistsAsync(Constants.DefaultUserRole))
            {
                await roleManager.CreateAsync(new IdentityRole(Constants.DefaultUserRole));
            }
        }
    }
}
