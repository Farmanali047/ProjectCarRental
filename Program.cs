using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using ProjectCarRental.Data;
using ProjectCarRental.Models;
using ProjectCarRental.Models.Interfaces;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<myAppUsers>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireClaim(ClaimTypes.Email,"Admin@gmail.com"));
});
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IBookingform, BookingRepository>();
builder.Services.AddScoped<ICarRegisteration, CarRegisterationRepository>();
builder.Services.AddScoped<IRepository<Insurance>>(provider => new GenericRepository<Insurance>("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=booking;Integrated Security=True;"));
builder.Services.AddScoped<IRepository<Pakage>>(provider => new GenericRepository<Pakage>("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=booking;Integrated Security=True;"));
builder.Services.AddScoped<IRepository<Bookingform>>(provider => new GenericRepository<Bookingform>("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=booking;Integrated Security=True;"));
builder.Services.AddScoped<IRepository<CarRegisteration>>(provider => new GenericRepository<CarRegisteration>("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=booking;Integrated Security=True;"));
builder.Services.AddScoped<IRepository<AspNetUsers>>(provider => new GenericRepository<AspNetUsers>("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=User;Integrated Security=True;"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
