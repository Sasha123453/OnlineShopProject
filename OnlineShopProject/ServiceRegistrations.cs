

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using OnlineShop.Db;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShop.Db.Services;
using OnlineShopProject.Areas.Identity.Services;
using OnlineShopProject.Interfaces;
using OnlineShopProject.Services;

public static class ServicesRegistration
{
    public static void AddMyServices(this IServiceCollection services)
    {
        services.AddTransient<IEmailSender<User>, IdentityEmailSender>();
        services.AddTransient<IEmailSender, EmailSender>();
        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromDays(1);
        });

        services.AddScoped<ICartsRepository, CartsRepository>();
        services.AddTransient<IViewRequests, ViewRequests>();
        services.AddTransient<IProductsRepository, ProductsRepository>();
        services.AddTransient<IUsersService, UsersService>();
        services.AddTransient<IOrdersRepository, OrdersRepository>();
        services.AddTransient<IAddressesRepository, AddressesRepository>();
        services.AddTransient<IComparsionRepository, ComparsionRepository>();
        services.AddTransient<IFavoriteRepository, FavoriteRepository>();
        services.AddTransient<ISessionsRepository<Cart>, SessionsCartRepository>();
    }
}