using OnlineShopProject;
using OnlineShopProject.InMemoryModels;
using OnlineShop.Db;
using OnlineShopProject.Interfaces;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Services;
using OnlineShop.Db.Models;
using Microsoft.EntityFrameworkCore;
using OnlineShopProject.Mappers;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));
builder.Services.AddDbContext<IdentityContext>(options => options.UseSqlServer(connection));
builder.Services.AddIdentity<User,IdentityRole>().AddEntityFrameworkStores<IdentityContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICartsRepository, CartsRepository>();
builder.Services.AddTransient<IGenericRequests, GenericRequests>();
builder.Services.AddSingleton<IConstances,Constances>();
builder.Services.AddSingleton<UserOrders>();
builder.Services.AddTransient<IProductsRepository, ProductsRepository>();
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddTransient<IOrdersRepository, OrdersRepository>();
builder.Services.AddTransient<IAddressesRepository, AddressesRepository>();
builder.Services.AddTransient<IComparsionRepository, ComparsionRepository>();
builder.Services.AddTransient<IFavoriteRepository, FavoriteRepository>();
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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Shop}/{action=ProductsPage}/{id?}");

app.Run();
