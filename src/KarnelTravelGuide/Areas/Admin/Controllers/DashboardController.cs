using KarnelTravelGuide.Areas.Admin.Models;
using KarnelTravelGuide.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KarnelTravelGuide.Areas.Admin.Controllers;

/// <summary>/Admin: counts and the latest bookings.</summary>
[Area("Admin")]
[Authorize]
public class DashboardController : Controller
{
    private readonly KarnelDbContext _db;

    public DashboardController(KarnelDbContext db) => _db = db;

    public async Task<IActionResult> Index()
    {
        var latest = await _db.Bookings.Include(b => b.Customer)
            .OrderByDescending(b => b.CreatedAt).Take(5).ToListAsync();

        var model = new DashboardViewModel
        {
            NewBookings = await _db.Bookings.CountAsync(b => b.Status == "New"),
            UnreadFeedback = await _db.Feedbacks.CountAsync(f => !f.IsRead),
            Spots = await _db.TouristSpots.CountAsync(),
            Hotels = await _db.Hotels.CountAsync(),
            Restaurants = await _db.Restaurants.CountAsync(),
            Resorts = await _db.Resorts.CountAsync(),
            Routes = await _db.Transports.CountAsync(),
            Packages = await _db.Packages.CountAsync(),
            LatestBookings = await BookingsController.WithServiceNames(_db, latest)
        };
        return View(model);
    }
}
