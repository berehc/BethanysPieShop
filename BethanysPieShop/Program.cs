// SERVICE REGISTRATION //
using System.Runtime.CompilerServices;
using BethanysPieShop.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPieRepository, PieRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IShoppingCart, ShoppingCart>(sp => ShoppingCart.GetCart(sp));
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();


builder.Services.AddControllersWithViews(); //enables MVC in my app
builder.Services.AddRazorPages();
//Configuration to use EFCore
builder.Services.AddDbContext<BethanysPieShopDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration["ConnectionStrings:BethanysPieShopDbContextConnection"]);
});

var app = builder.Build();


//SETTING UP MIDDLEWARE //

// middleware component that is preconfigured to look for incoming requests for static files, such as a JPEG or CSS file. It will look in that default configured folder wwwroot for static file and return it. It will then also short circuit the request
app.UseStaticFiles();
app.UseSession();


//diagnostic middleware component that rwill not always show the exception page, but it´s called the DEveloper Exception Page.Contains information only for developers. That Page only will showed if the app is running in a Developer enviroment.

if(app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}


/* middleware to add the ability to be able to navigate to our pages to make sure that ASP:NET Core will be able to handle incoming reuqest correctly. This will set again some defaults used typically in MVC to route to the views that we are going to have.
 this is our endpoint middleware  {controller=Home}/{action=Index}/{id?} */
app.MapDefaultControllerRoute();

//MapControllerRoute can be used also

// app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
DbInitializer.Seed(app);

app.Run();
