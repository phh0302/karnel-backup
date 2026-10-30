using KarnelTravelGuide.Data;
using KarnelTravelGuide.Helpers;
using KarnelTravelGuide.Models;
using KarnelTravelGuide.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KarnelTravelGuide.Controllers;

/// <summary>
/// Information Page and its five sections: Tourist Spots, Travel, Hotels,
/// Restaurants and Resorts, plus a detail page for each place (CRS FR-40 to FR-47).
/// </summary>
public class InformationController : Controller
{
    private readonly KarnelDbContext _db;

    public InformationController(KarnelDbContext db) => _db = db;

    // GET /Information
    // Five links, new hotels / restaurants / resorts, and current packages (Algorithm A1).
    public async Task<IActionResult> Index()
    {
        var today = DateTime.Today;

        var model = new InformationIndexViewModel
        {
            NewHotels = (await _db.Hotels.Include(x => x.Location).Where(x => x.IsNew)
                .OrderByDescending(x => x.CreatedAt).Take(4).ToListAsync()).Select(Cards.From).ToList(),
            NewRestaurants = (await _db.Restaurants.Include(x => x.Location).Where(x => x.IsNew)
                .OrderByDescending(x => x.CreatedAt).Take(4).ToListAsync()).Select(Cards.From).ToList(),
            NewResorts = (await _db.Resorts.Include(x => x.Location).Where(x => x.IsNew)
                .OrderByDescending(x => x.CreatedAt).Take(4).ToListAsync()).Select(Cards.From).ToList(),
            Packages = (await _db.Packages.Include(x => x.Location)
                .Where(p => p.IsActive && p.ValidFrom <= today && p.ValidUntil >= today)
                .OrderByDescending(p => p.DiscountPercent).ToListAsync()).Select(Cards.From).ToList(),
            SpotCount = await _db.TouristSpots.CountAsync(),
            RouteCount = await _db.Transports.CountAsync(),
            HotelCount = await _db.Hotels.CountAsync(),
            RestaurantCount = await _db.Restaurants.CountAsync(),
            ResortCount = await _db.Resorts.CountAsync()
        };
        return View(model);
    }

    // GET /Information/TouristSpots?locationId=5&category=Beach
    public async Task<IActionResult> TouristSpots(int? locationId, string? category)
    {
        var query = _db.TouristSpots.Include(s => s.Location).AsQueryable();
        if (locationId.HasValue) query = query.Where(s => s.LocationId == locationId);
        if (!string.IsNullOrEmpty(category)) query = query.Where(s => s.Category == category);

        var spots = await query.OrderByDescending(s => s.IsFeatured).ThenBy(s => s.Name).ToListAsync();
        var model = await ListModel("Tourist Spots", "Beaches, monuments, mountains and temples within the country.",
            nameof(TouristSpots), locationId, spots.Select(Cards.From));
        model.Categories = TouristSpot.Categories;
        model.Category = category;
        return View("PlaceList", model);
    }

    // GET /Information/Travel?fromId=1&toId=2&type=Bus
    public async Task<IActionResult> Travel(int? fromId, int? toId, string? type)
    {
        var query = _db.Transports.Include(t => t.FromLocation).Include(t => t.ToLocation).AsQueryable();
        if (fromId.HasValue) query = query.Where(t => t.FromLocationId == fromId);
        if (toId.HasValue) query = query.Where(t => t.ToLocationId == toId);
        if (!string.IsNullOrEmpty(type)) query = query.Where(t => t.TransportType == type);

        var model = new TravelViewModel
        {
            Routes = await query.OrderBy(t => t.FromLocation!.City).ThenBy(t => t.ToLocation!.City).ThenBy(t => t.Fare).ToListAsync(),
            Locations = await _db.Locations.OrderBy(l => l.City).ToListAsync(),
            FromId = fromId,
            ToId = toId,
            Type = type
        };
        return View(model);
    }

    // GET /Information/Hotels?locationId=5
    public async Task<IActionResult> Hotels(int? locationId)
    {
        var query = _db.Hotels.Include(h => h.Location).AsQueryable();
        if (locationId.HasValue) query = query.Where(h => h.LocationId == locationId);
        var list = await query.OrderBy(h => h.Location!.City).ThenByDescending(h => h.Stars).ToListAsync();
        return View("PlaceList", await ListModel("Hotel Information", "Hotels within the country, with price per night and rooms left.",
            nameof(Hotels), locationId, list.Select(Cards.From)));
    }

    // GET /Information/Restaurants?locationId=5
    public async Task<IActionResult> Restaurants(int? locationId)
    {
        var query = _db.Restaurants.Include(r => r.Location).AsQueryable();
        if (locationId.HasValue) query = query.Where(r => r.LocationId == locationId);
        var list = await query.OrderBy(r => r.Location!.City).ThenByDescending(r => r.Rating).ToListAsync();
        return View("PlaceList", await ListModel("Restaurant Information", "Restaurants within the country, from street food to fine dining.",
            nameof(Restaurants), locationId, list.Select(Cards.From)));
    }

    // GET /Information/Resorts?locationId=5
    public async Task<IActionResult> Resorts(int? locationId)
    {
        var query = _db.Resorts.Include(r => r.Location).AsQueryable();
        if (locationId.HasValue) query = query.Where(r => r.LocationId == locationId);
        var list = await query.OrderBy(r => r.Location!.City).ThenByDescending(r => r.Stars).ToListAsync();
        return View("PlaceList", await ListModel("Resorts Information", "Resorts within the country: beach villas, mountain lodges and island retreats.",
            nameof(Resorts), locationId, list.Select(Cards.From)));
    }

    // ------------------------------------------------------------------
    // Detail pages. Methods are named ...Detail so they do not hide the
    // entity class names; [ActionName] keeps the short URLs from the sitemap.
    // ------------------------------------------------------------------

    // GET /Information/Spot/4
    [ActionName("Spot")]
    public async Task<IActionResult> SpotDetail(int id)
    {
        var s = await _db.TouristSpots.Include(x => x.Location).FirstOrDefaultAsync(x => x.SpotId == id);
        if (s == null) return NotFound();

        // Places to stay in the same city, shown under the spot
        var hotels = await _db.Hotels.Include(h => h.Location).Where(h => h.LocationId == s.LocationId).Take(3).ToListAsync();
        var resorts = await _db.Resorts.Include(r => r.Location).Where(r => r.LocationId == s.LocationId).Take(3).ToListAsync();

        var model = new DetailViewModel
        {
            Section = "Tourist Spots", SectionAction = nameof(TouristSpots),
            Title = s.Name, Subtitle = $"{s.Category} · {s.Location?.City}, {s.Location?.Province}",
            ImageUrl = s.ImageUrl, Description = s.Description,
            Facts =
            {
                ("Category", s.Category),
                ("Address", s.Address ?? "-"),
                ("Entry fee", Money.Vnd(s.EntryFee, zeroIsFree: true)),
                ("Best time to visit", s.BestTime ?? "-")
            },
            RelatedTitle = $"Places to stay in {s.Location?.City}",
            Related = hotels.Select(Cards.From).Concat(resorts.Select(Cards.From)).ToList()
        };
        return View("Detail", model);
    }

    // GET /Information/Hotel/7
    [ActionName("Hotel")]
    public async Task<IActionResult> HotelDetail(int id)
    {
        var h = await _db.Hotels.Include(x => x.Location).FirstOrDefaultAsync(x => x.HotelId == id);
        if (h == null) return NotFound();
        var model = new DetailViewModel
        {
            Section = "Hotels", SectionAction = nameof(Hotels),
            Title = h.Name, Subtitle = $"{h.Location?.City}, {h.Location?.Province}",
            ImageUrl = h.ImageUrl, Description = h.Description, Stars = h.Stars, Badge = h.IsNew ? "New" : null,
            Facts =
            {
                ("Price per night", Money.Vnd(h.PricePerNight)),
                ("Rooms available", h.RoomsAvailable > 0 ? $"{h.RoomsAvailable} of {h.TotalRooms}" : "Full"),
                ("Guest rating", h.Rating.HasValue ? $"{h.Rating:0.0} / 5" : "-"),
                ("Address", h.Address ?? "-"),
                ("Phone", h.Phone ?? "-")
            },
            BookServiceType = "Hotel", BookServiceId = h.HotelId
        };
        await AddSpotsNearby(model, h.LocationId);
        return View("Detail", model);
    }

    // GET /Information/Restaurant/3
    [ActionName("Restaurant")]
    public async Task<IActionResult> RestaurantDetail(int id)
    {
        var r = await _db.Restaurants.Include(x => x.Location).FirstOrDefaultAsync(x => x.RestaurantId == id);
        if (r == null) return NotFound();
        var model = new DetailViewModel
        {
            Section = "Restaurants", SectionAction = nameof(Restaurants),
            Title = r.Name, Subtitle = $"{r.Cuisine} · {r.Location?.City}",
            ImageUrl = r.ImageUrl, Description = r.Description, Badge = r.IsNew ? "New" : null,
            Facts =
            {
                ("Cuisine", r.Cuisine),
                ("Price level", Money.PriceLevel(r.PriceLevel)),
                ("Average per person", Money.Vnd(r.AvgPricePerPerson)),
                ("Seats available", r.SeatsAvailable > 0 ? $"{r.SeatsAvailable} of {r.TotalSeats}" : "Full"),
                ("Guest rating", r.Rating.HasValue ? $"{r.Rating:0.0} / 5" : "-"),
                ("Opening hours", r.OpenHours ?? "-"),
                ("Address", r.Address ?? "-"),
                ("Phone", r.Phone ?? "-")
            },
            BookServiceType = "Restaurant", BookServiceId = r.RestaurantId
        };
        await AddSpotsNearby(model, r.LocationId);
        return View("Detail", model);
    }

    // GET /Information/Resort/2
    [ActionName("Resort")]
    public async Task<IActionResult> ResortDetail(int id)
    {
        var r = await _db.Resorts.Include(x => x.Location).FirstOrDefaultAsync(x => x.ResortId == id);
        if (r == null) return NotFound();
        var model = new DetailViewModel
        {
            Section = "Resorts", SectionAction = nameof(Resorts),
            Title = r.Name, Subtitle = $"{r.Location?.City}, {r.Location?.Province}",
            ImageUrl = r.ImageUrl, Description = r.Description, Stars = r.Stars, Badge = r.IsNew ? "New" : null,
            Facts =
            {
                ("Price per night", Money.Vnd(r.PricePerNight)),
                ("Villas available", r.UnitsAvailable > 0 ? $"{r.UnitsAvailable} of {r.TotalUnits}" : "Full"),
                ("Facilities", r.Facilities ?? "-"),
                ("Guest rating", r.Rating.HasValue ? $"{r.Rating:0.0} / 5" : "-"),
                ("Address", r.Address ?? "-"),
                ("Phone", r.Phone ?? "-")
            },
            BookServiceType = "Resort", BookServiceId = r.ResortId
        };
        await AddSpotsNearby(model, r.LocationId);
        return View("Detail", model);
    }

    // GET /Information/Package/1
    [ActionName("Package")]
    public async Task<IActionResult> PackageDetail(int id)
    {
        var p = await _db.Packages.Include(x => x.Location)
            .Include(x => x.PackageSpots).ThenInclude(ps => ps.Spot).ThenInclude(s => s!.Location)
            .FirstOrDefaultAsync(x => x.PackageId == id);
        if (p == null) return NotFound();

        var model = new DetailViewModel
        {
            Section = "Packages", SectionAction = nameof(Index),
            Title = p.Title, Subtitle = $"{p.DurationDays} days · starts in {p.Location?.City}",
            ImageUrl = p.ImageUrl, Description = p.Description,
            Badge = p.DiscountPercent > 0 ? $"-{p.DiscountPercent}%" : null,
            Facts =
            {
                ("Price", p.DiscountPercent > 0
                    ? $"{Money.Vnd(p.FinalPrice)} (was {Money.Vnd(p.Price)})"
                    : Money.Vnd(p.Price)),
                ("Duration", $"{p.DurationDays} days"),
                ("Valid", $"{p.ValidFrom:dd/MM/yyyy} – {p.ValidUntil:dd/MM/yyyy}"),
                ("Status", p.IsActive ? "Open for booking" : "Not available")
            },
            RelatedTitle = "Places this tour visits",
            Related = p.PackageSpots.OrderBy(ps => ps.DayNumber)
                .Where(ps => ps.Spot != null)
                .Select(ps => { var c = Cards.From(ps.Spot!); c.Badge = $"Day {ps.DayNumber}"; return c; })
                .ToList(),
            BookServiceType = p.IsActive ? "Package" : null, BookServiceId = p.PackageId
        };
        return View("Detail", model);
    }

    // ---- helpers ----

    /// <summary>Builds the model for a card list page with the city filter.</summary>
    private async Task<PlaceListViewModel> ListModel(string title, string intro, string action, int? locationId, IEnumerable<PlaceCard> cards)
        => new()
        {
            Title = title,
            Intro = intro,
            Action = action,
            Cards = cards.ToList(),
            Locations = await _db.Locations.OrderBy(l => l.City).ToListAsync(),
            LocationId = locationId
        };

    /// <summary>Adds up to 4 tourist spots in the same city as "Things to see nearby".</summary>
    private async Task AddSpotsNearby(DetailViewModel model, int locationId)
    {
        var spots = await _db.TouristSpots.Include(s => s.Location)
            .Where(s => s.LocationId == locationId).Take(4).ToListAsync();
        model.RelatedTitle = "Things to see nearby";
        model.Related = spots.Select(Cards.From).ToList();
    }
}
