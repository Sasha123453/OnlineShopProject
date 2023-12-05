using OnlineShopProject;
using OnlineShopProject.InMemoryModels;
using OnlineShop.Db;
using OnlineShopProject.Interfaces;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Services;
using OnlineShop.Db.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<ICartsRepository, CartsRepository>();
builder.Services.AddSingleton<IConstances,Constances>();
builder.Services.AddSingleton<UserOrders>();
builder.Services.AddTransient<IProductsRepository, ProductsRepository>();
builder.Services.AddSingleton<IOrdersRepository, OrdersRepository>();
builder.Services.AddSingleton<IAddressesRepository, AddressesRepository>();
builder.Services.AddSingleton<IComparsionRepository, ComparsionRepository>();
builder.Services.AddSingleton<IFavoriteRepository, FavoriteRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Shop}/{action=ProductsPage}/{id?}");

app.Run();
