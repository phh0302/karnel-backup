// =====================================================================
// Karnel Travel Guide - application start-up
// Registers services (database, admin login, MVC) and the URL routes.
// =====================================================================
using System.Globalization;
using KarnelTravelGuide.Data;
using KarnelTravelGuide.Models.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ---- Services ----

// MVC controllers with views
builder.Services.AddControllersWithViews();

// Database: SQL Server connection string "KarnelDb" from appsettings.json
builder.Services.AddDbContext<KarnelDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("KarnelDb")));

// Admin login with a cookie. Pages marked [Authorize] send anonymous users to the login page.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Admin/Account/Login";
        options.LogoutPath = "/Admin/Account/Logout";
        options.AccessDeniedPath = "/Admin/Account/Login";
        options.Cookie.Name = "KarnelAdmin";
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
    });
builder.Services.AddAuthorization();

// Password hasher that verifies the hashes stored in Admins.PasswordHash
builder.Services.AddScoped<IPasswordHasher<Admin>, PasswordHasher<Admin>>();

var app = builder.Build();

// ---- Culture ----
// Use en-US for parsing form input so numbers like "4.5" and dates "2026-12-01" bind the same
// way on every PC, whatever the Windows regional setting is. Prices are still shown in VND
// format by the Money helper.
var enUs = new CultureInfo("en-US");
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(enUs),
    SupportedCultures = new[] { enUs },
    SupportedUICultures = new[] { enUs }
});

// ---- Request pipeline ----
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
// 404 and other error codes show a friendly page instead of a blank one
app.UseStatusCodePagesWithReExecute("/Home/Missing");

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// ---- Routes ----
// Admin area: /Admin/{controller}/{action}/{id?}, default = Dashboard
app.MapAreaControllerRoute(
    name: "adminArea",
    areaName: "Admin",
    pattern: "Admin/{controller=Dashboard}/{action=Index}/{id?}");

// Short public addresses from the sitemap
app.MapControllerRoute("about", "About", new { controller = "Home", action = "About" });

// Default route: /{controller}/{action}/{id?}, default = Home/Index
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
