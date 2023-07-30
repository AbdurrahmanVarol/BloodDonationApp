using BloodDonationApp.Business;
using BloodDonationApp.DataAccess;
using BloodDonationApp.DataAccess.Entityframework.Contexts;
using BloodDonationApp.DataAccess.Entityframework.Seeding;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        option.LoginPath = "/auth/login";
        option.AccessDeniedPath = "/auth/AccessDenied";
    });
builder.Services.AddDataAccessServices(builder.Configuration);
builder.Services.AddBusinessServices();

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
    pattern: "{controller=auth}/{action=login}/{id?}");

using var scope = app.Services.GetService<IServiceScopeFactory>()?.CreateScope();
var services = scope?.ServiceProvider;
var context = services?.GetRequiredService<BloodDonationAppContext>();
context?.Database.EnsureCreated();
EfDbSeeding.SeedDatabase(context);

app.Run();
