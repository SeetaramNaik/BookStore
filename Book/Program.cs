using Microsoft.EntityFrameworkCore;
using Application.DataAccess.Repository.iRepository;
using Application.DataAccess.Repository;
using Application.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Book.Models;
using Book.Utility;
using Microsoft.AspNetCore.Identity.UI.Services;


var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

//Configuring Swagger
builder.Services.AddEndpointsApiExplorer();

// Add services to the container.
builder.Services.AddControllersWithViews();



//Swagger Documentation Section
var info = new OpenApiInfo()
{
    Title = "Book Store API Documentation",
    Version = "v1",
    Description = "List of all APIs used for Book Store Application",
    Contact = new OpenApiContact()
    {
        Name = "Seetaram Naik",
        Email = "seetaramnaik45@gmail.com",
    }

};

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", info);

    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});


builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true).AddDefaultTokenProviders().AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<IUnitofWork, UnitOfWork>();

builder.Services.AddSingleton<IEmailSender, EmailSender>();

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();

}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "Home",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.UseSwagger(u =>
{
    u.RouteTemplate = "swagger/{documentName}/swagger.json";
});

app.UseSwaggerUI(c =>
{
    c.RoutePrefix = "swagger";
    c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "Book Store API");
});

app.Run();
