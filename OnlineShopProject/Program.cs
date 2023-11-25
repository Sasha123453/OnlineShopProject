using OnlineShopProject;
using OnlineShopProject.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<ICartsRepository, CartsRepository>();
builder.Services.AddSingleton<IConstances, Constances>();
builder.Services.AddSingleton<IProductsRepository, ProductsRepository>();
builder.Services.AddSingleton<IOrdersRepository, OrdersRepository>();
builder.Services.AddSingleton<IComparsionRepository, ComparsionRepository>();
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
