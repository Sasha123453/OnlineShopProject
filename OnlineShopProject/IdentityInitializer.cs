using Microsoft.AspNetCore.Identity;
using OnlineShop.Db;
using OnlineShop.Db.Models;

namespace OnlineShopProject
{
    public class IdentityInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminLogin = "admin@123";
            string password = "Passwd123";
            if (roleManager.FindByNameAsync(Constants.DefaultAdminRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(Constants.DefaultAdminRole));
            }
            if (userManager.FindByNameAsync(adminLogin) == null)
            {
                User admin = new User { UserName = adminLogin };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, Constants.DefaultAdminRole);
                }
            }
            if (roleManager.FindByNameAsync(Constants.DefaultUserRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(Constants.DefaultUserRole));
            }
        }
    }
}
