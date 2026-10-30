using KarnelTravelGuide.Areas.Admin.Models;
using KarnelTravelGuide.Data;
using KarnelTravelGuide.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KarnelTravelGuide.Areas.Admin.Controllers;

/// <summary>
/// /Admin/Bookings: view orders and change their status (CRS FR-62, Algorithm A8).
/// </summary>
[Area("Admin")]
[Authorize]
public class BookingsController : Controller
{
    private readonly KarnelDbContext _db;

    public BookingsController(KarnelDbContext db) => _db = db;

    // Allowed status changes (A8): New → Contacted → Completed; New or Contacted → Cancelled
    private static readonly Dictionary<string, string[]> Allowed = new()
    {
        ["New"] = new[] { "Contacted", "Cancelled" },
        ["Contacted"] = new[] { "Completed", "Cancelled" },
        ["Completed"] = Array.Empty<string>(),
        ["Cancelled"] = Array.Empty<string>()
    };

    /// <summary>Next statuses a booking may move to; used by the view to show buttons.</summary>
    public static string[] NextStatuses(string status) => Allowed.TryGetValue(status, out var next) ? next : Array.Empty<string>();

    // GET /Admin/Bookings?status=New
    public async Task<IActionResult> Index(string? status)
    {
        var query = _db.Bookings.Include(b => b.Customer).AsQueryable();
        if (!string.IsNullOrEmpty(status)) query = query.Where(b => b.Status == status);
        var list = await query.OrderByDescending(b => b.CreatedAt).ToListAsync();

        var model = new BookingsViewModel
        {
            Rows = await WithServiceNames(_db, list),
            Status = status,
            Counts = await _db.Bookings.GroupBy(b => b.Status).Select(g => new { g.Key, N = g.Count() })
                .ToDictionaryAsync(x => x.Key, x => x.N)
        };
        return View(model);
    }

    // POST /Admin/Bookings/UpdateStatus  (Algorithm A8)
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateStatus(int id, string newStatus, string? returnStatus)
    {
        var booking = await _db.Bookings.FindAsync(id);
        if (booking == null) return NotFound();

        if (!NextStatuses(booking.Status).Contains(newStatus))
        {
            TempData["Error"] = $"Booking #{id} cannot go from {booking.Status} to {newStatus}.";
            return RedirectToAction(nameof(Index), new { status = returnStatus });
        }

        // One transaction: availability and status change together or not at all
        await using var tx = await _db.Database.BeginTransactionAsync();

        if (newStatus == "Contacted")
        {
            // Staff confirmed with the customer: hold the rooms / seats / villas
            string? problem = await ChangeCapacity(booking, -1);
            if (problem != null)
            {
                await tx.RollbackAsync();
                TempData["Error"] = problem;
                return RedirectToAction(nameof(Index), new { status = returnStatus });
            }
        }
        else if (newStatus == "Cancelled" && booking.Status == "Contacted")
        {
            // Give back what was held when the booking was confirmed
            await ChangeCapacity(booking, +1);
        }

        booking.Status = newStatus;
        await _db.SaveChangesAsync();
        await tx.CommitAsync();

        TempData["Ok"] = $"Booking #{id} is now {newStatus}.";
        return RedirectToAction(nameof(Index), new { status = returnStatus });
    }

    /// <summary>
    /// Adds (direction +1) or removes (direction -1) the booked amount from the service's availability.
    /// Returns an error message if there is not enough left, otherwise null. Packages have no capacity.
    /// </summary>
    private async Task<string?> ChangeCapacity(Booking b, int direction)
    {
        switch (b.ServiceType)
        {
            case "Hotel":
                var h = await _db.Hotels.FindAsync(b.ServiceId);
                if (h == null) return null;
                if (direction < 0 && h.RoomsAvailable < b.Quantity) return $"Only {h.RoomsAvailable} rooms left at {h.Name}.";
                h.RoomsAvailable = Math.Min(h.TotalRooms, h.RoomsAvailable + direction * b.Quantity);
                break;
            case "Resort":
                var r = await _db.Resorts.FindAsync(b.ServiceId);
                if (r == null) return null;
                if (direction < 0 && r.UnitsAvailable < b.Quantity) return $"Only {r.UnitsAvailable} villas left at {r.Name}.";
                r.UnitsAvailable = Math.Min(r.TotalUnits, r.UnitsAvailable + direction * b.Quantity);
                break;
            case "Restaurant":
                var rs = await _db.Restaurants.FindAsync(b.ServiceId);
                if (rs == null) return null;
                if (direction < 0 && rs.SeatsAvailable < b.NumberOfPeople) return $"Only {rs.SeatsAvailable} seats left at {rs.Name}.";
                rs.SeatsAvailable = Math.Min(rs.TotalSeats, rs.SeatsAvailable + direction * b.NumberOfPeople);
                break;
            case "Transport":
                var t = await _db.Transports.FindAsync(b.ServiceId);
                if (t == null) return null;
                if (direction < 0 && t.SeatsAvailable < b.NumberOfPeople) return $"Only {t.SeatsAvailable} seats left on this route.";
                t.SeatsAvailable += direction * b.NumberOfPeople;
                break;
        }
        await _db.SaveChangesAsync();
        return null;
    }

    /// <summary>
    /// Looks up the readable name of each booked service (Bookings.ServiceId points to
    /// different tables depending on ServiceType). Shared with the dashboard.
    /// </summary>
    public static async Task<List<BookingRow>> WithServiceNames(KarnelDbContext db, List<Booking> bookings)
    {
        List<int> Ids(string type) => bookings.Where(b => b.ServiceType == type).Select(b => b.ServiceId).Distinct().ToList();

        var hotelIds = Ids("Hotel");
        var restaurantIds = Ids("Restaurant");
        var resortIds = Ids("Resort");
        var transportIds = Ids("Transport");
        var packageIds = Ids("Package");

        var names = new Dictionary<(string, int), string>();
        foreach (var x in await db.Hotels.Where(x => hotelIds.Contains(x.HotelId)).Select(x => new { x.HotelId, x.Name }).ToListAsync())
            names[("Hotel", x.HotelId)] = x.Name;
        foreach (var x in await db.Restaurants.Where(x => restaurantIds.Contains(x.RestaurantId)).Select(x => new { x.RestaurantId, x.Name }).ToListAsync())
            names[("Restaurant", x.RestaurantId)] = x.Name;
        foreach (var x in await db.Resorts.Where(x => resortIds.Contains(x.ResortId)).Select(x => new { x.ResortId, x.Name }).ToListAsync())
            names[("Resort", x.ResortId)] = x.Name;
        foreach (var x in await db.Transports.Where(x => transportIds.Contains(x.TransportId))
                     .Select(x => new { x.TransportId, From = x.FromLocation!.City, To = x.ToLocation!.City, x.TransportType }).ToListAsync())
            names[("Transport", x.TransportId)] = $"{x.From} → {x.To} ({x.TransportType})";
        foreach (var x in await db.Packages.Where(x => packageIds.Contains(x.PackageId)).Select(x => new { x.PackageId, x.Title }).ToListAsync())
            names[("Package", x.PackageId)] = x.Title;

        return bookings.Select(b => new BookingRow
        {
            Booking = b,
            ServiceName = names.TryGetValue((b.ServiceType, b.ServiceId), out var n) ? n : "(removed)"
        }).ToList();
    }
}
