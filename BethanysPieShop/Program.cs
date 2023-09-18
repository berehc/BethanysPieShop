// SERVICE REGISTRATION //
using System.Runtime.CompilerServices;
using BethanysPieShop.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICategoryRepository, MockCategoryRepository>();
builder.Services.AddScoped<IPieRepository, MockPieRepository>();

builder.Services.AddControllersWithViews(); //enables MVC in my app

var app = builder.Build();

//SETTING UP MIDDLEWARE //

//app.MapGet("/", () => "Hello World!"); //listens for an incoming request to the root of the application and will return "Hello World"


// middleware component that is preconfigured to look for incoming requests for static files, such as a JPEG or CSS file. It will look in that default configured folder wwwroot for static file and return it. It will then also short circuit the request
app.UseStaticFiles();  


//diagnostic middleware component that rwill not always show the exception page, but it´s called the DEveloper Exception Page.Contains information only for developers. That Page only will showed if the app is running in a Developer enviroment.

if(app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}


//middleware to add the ability to be able to navigate to our pages to make sure that ASP:NET Core will be able to handle incoming reuqest correctly. This will set again some defaults used typically in MVC to route to the views that we are going to have.
// this is our endpoint middleware
app.MapDefaultControllerRoute();

app.Run();
