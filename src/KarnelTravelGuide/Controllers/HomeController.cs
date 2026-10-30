using KarnelTravelGuide.Data;
using KarnelTravelGuide.Helpers;
using KarnelTravelGuide.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KarnelTravelGuide.Controllers;

/// <summary>
/// Home Page and About Us page (CRS FR-10, FR-11, FR-20, FR-21).
/// </summary>
public class HomeController : Controller
{
    private readonly KarnelDbContext _db;

    public HomeController(KarnelDbContext db) => _db = db;

    // GET /
    // Shows the quick search, featured tourist spots and current packages.
    public async Task<IActionResult> Index()
    {
        var today = DateTime.Today;

        // Featured spots first, then the newest; at most 8 cards
        var spots = await _db.TouristSpots.Include(s => s.Location)
            .OrderByDescending(s => s.IsFeatured).ThenBy(s => s.Name)
            .Take(8).ToListAsync();

        // Packages that are active today, biggest discount first (Algorithm A1)
        var packages = await _db.Packages.Include(p => p.Location)
            .Where(p => p.IsActive && p.ValidFrom <= today && p.ValidUntil >= today)
            .OrderByDescending(p => p.DiscountPercent)
            .Take(3).ToListAsync();

        var model = new HomeViewModel
        {
            FeaturedSpots = spots.Select(Cards.From).ToList(),
            Packages = packages.Select(Cards.From).ToList(),
            Locations = await _db.Locations.OrderBy(l => l.City).ToListAsync()
        };
        return View(model);
    }

    // GET /About
    // Company details and the facilities it provides, with live counts from the database.
    public async Task<IActionResult> About()
    {
        var model = new AboutViewModel
        {
            Destinations = await _db.Locations.CountAsync(),
            Hotels = await _db.Hotels.CountAsync(),
            Resorts = await _db.Resorts.CountAsync(),
            Routes = await _db.Transports.CountAsync(),
            Packages = await _db.Packages.CountAsync(p => p.IsActive)
        };
        return View(model);
    }

    // Shown for 404 and other status codes (see Program.cs)
    public IActionResult Missing() => View();

    // Shown when an unexpected error happens in production
    public IActionResult Error() => View();
}
