using System.Security.Claims;
using KarnelTravelGuide.Areas.Admin.Models;
using KarnelTravelGuide.Data;
using KarnelTravelGuide.Models.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KarnelTravelGuide.Areas.Admin.Controllers;

/// <summary>
/// Admin login and logout (CRS FR-60, Algorithm A7).
/// </summary>
[Area("Admin")]
[AllowAnonymous]
public class AccountController : Controller
{
    private readonly KarnelDbContext _db;
    private readonly IPasswordHasher<Admin> _hasher;

    public AccountController(KarnelDbContext db, IPasswordHasher<Admin> hasher)
    {
        _db = db;
        _hasher = hasher;
    }

    // GET /Admin/Account/Login
    public IActionResult Login(string? returnUrl)
    {
        if (User.Identity?.IsAuthenticated == true) return RedirectToAction("Index", "Dashboard");
        return View(new LoginForm { ReturnUrl = returnUrl });
    }

    // POST /Admin/Account/Login
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginForm form)
    {
        if (!ModelState.IsValid) return View(form);

        var admin = await _db.Admins.FirstOrDefaultAsync(a => a.Username == form.Username.Trim());

        // Same message whether the username or the password is wrong, so it does not reveal which usernames exist
        if (admin == null || _hasher.VerifyHashedPassword(admin, admin.PasswordHash, form.Password) == PasswordVerificationResult.Failed)
        {
            ModelState.AddModelError("", "Username or password is incorrect.");
            return View(form);
        }

        admin.LastLoginAt = DateTime.Now;
        await _db.SaveChangesAsync();

        // Create the login cookie with the admin's id and name
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, admin.AdminId.ToString()),
            new(ClaimTypes.Name, admin.FullName),
            new("username", admin.Username)
        };
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

        // Only follow local return URLs (prevents redirecting to another site)
        if (!string.IsNullOrEmpty(form.ReturnUrl) && Url.IsLocalUrl(form.ReturnUrl)) return Redirect(form.ReturnUrl);
        return RedirectToAction("Index", "Dashboard");
    }

    // POST /Admin/Account/Logout
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction(nameof(Login));
    }
}
