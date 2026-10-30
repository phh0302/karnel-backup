using KarnelTravelGuide.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KarnelTravelGuide.Controllers;

/// <summary>
/// Home Page. At this stage it only proves the site can reach the database
/// by counting the rows of the main tables.
/// </summary>
public class HomeController : Controller
{
    private readonly KarnelDbContext _db;

    public HomeController(KarnelDbContext db) => _db = db;

    // GET /
    public async Task<IActionResult> Index()
    {
        // Count rows so we can see the database connection works
        ViewBag.Locations = await _db.Locations.CountAsync();
        ViewBag.Spots = await _db.TouristSpots.CountAsync();
        ViewBag.Hotels = await _db.Hotels.CountAsync();
        return View();
    }

    // Shown for 404 and other status codes (see Program.cs)
    public IActionResult Missing() => View();

    // Shown when an unexpected error happens in production
    public IActionResult Error() => View();
}
