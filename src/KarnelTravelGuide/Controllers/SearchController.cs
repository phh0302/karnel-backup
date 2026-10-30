using KarnelTravelGuide.Data;
using KarnelTravelGuide.Helpers;
using KarnelTravelGuide.Models;
using KarnelTravelGuide.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KarnelTravelGuide.Controllers;

/// <summary>
/// Search Page and Advanced Search (CRS FR-30 to FR-35).
/// Follows Algorithms A2 (tourist spots), A3 (hotels, restaurants, resorts) and A4 (advanced).
/// All filters are applied in the SQL query by Entity Framework (parameterised, NFR-07).
/// </summary>
public class SearchController : Controller
{
    private readonly KarnelDbContext _db;

    public SearchController(KarnelDbContext db) => _db = db;

    // GET /Search?type=Hotel&locationId=5&minPrice=500000&maxPrice=2500000&minQuality=3&quantity=2&onlyAvailable=true
    public async Task<IActionResult> Index([FromQuery] SearchCriteria c)
    {
        Normalise(c);

        // Pick the right search for the chosen tab
        List<PlaceCard> results = c.Type switch
        {
            "Spot" => (await SpotQuery(c).ToListAsync()).Select(Cards.From).ToList(),
            "Restaurant" => (await RestaurantQuery(c).OrderBy(r => r.AvgPricePerPerson).ToListAsync()).Select(Cards.From).ToList(),
            "Resort" => (await ResortQuery(c).OrderBy(r => r.PricePerNight).ToListAsync()).Select(Cards.From).ToList(),
            _ => (await HotelQuery(c).OrderBy(h => h.PricePerNight).ToListAsync()).Select(Cards.From).ToList()
        };

        var model = new SearchViewModel
        {
            Criteria = c,
            Locations = await _db.Locations.OrderBy(l => l.City).ToListAsync(),
            Results = results
        };
        return View(model);
    }

    // GET /Search/Advanced?keyword=beach&types=Hotel&types=Resort&sortBy=price&sortDir=asc&page=1
    public async Task<IActionResult> Advanced([FromQuery] SearchCriteria c)
    {
        Normalise(c);
        if (c.Types.Count == 0)
            c.Types = new List<string> { "Spot", "Hotel", "Restaurant", "Resort" };   // A4: no type = all types

        // Collect matching rows from every selected type (A4)
        var rows = new List<SearchRow>();
        if (c.Types.Contains("Spot"))
            rows.AddRange((await SpotQuery(c).ToListAsync()).Select(s => new SearchRow
            {
                Type = "Tourist spot", Name = s.Name, City = s.Location?.City ?? "", Price = s.EntryFee,
                PriceUnit = "entry", Quality = s.IsFeatured ? 1 : 0, QualityText = s.Category,
                Url = $"/Information/Spot/{s.SpotId}"
            }));
        if (c.Types.Contains("Hotel"))
            rows.AddRange((await HotelQuery(c).ToListAsync()).Select(h => new SearchRow
            {
                Type = "Hotel", Name = h.Name, City = h.Location?.City ?? "", Price = h.PricePerNight,
                PriceUnit = "night", Quality = h.Stars, QualityText = Money.Stars(h.Stars),
                Url = $"/Information/Hotel/{h.HotelId}"
            }));
        if (c.Types.Contains("Restaurant"))
            rows.AddRange((await RestaurantQuery(c).ToListAsync()).Select(r => new SearchRow
            {
                Type = "Restaurant", Name = r.Name, City = r.Location?.City ?? "", Price = r.AvgPricePerPerson,
                PriceUnit = "person", Quality = r.Rating ?? 0, QualityText = r.Rating.HasValue ? $"{r.Rating:0.0} / 5" : "-",
                Url = $"/Information/Restaurant/{r.RestaurantId}"
            }));
        if (c.Types.Contains("Resort"))
            rows.AddRange((await ResortQuery(c).ToListAsync()).Select(r => new SearchRow
            {
                Type = "Resort", Name = r.Name, City = r.Location?.City ?? "", Price = r.PricePerNight,
                PriceUnit = "night", Quality = r.Stars, QualityText = Money.Stars(r.Stars),
                Url = $"/Information/Resort/{r.ResortId}"
            }));

        // Sort (A4)
        bool desc = c.SortDir == "desc";
        IEnumerable<SearchRow> sorted = c.SortBy switch
        {
            "quality" => desc ? rows.OrderByDescending(r => r.Quality) : rows.OrderBy(r => r.Quality),
            "name" => desc ? rows.OrderByDescending(r => r.Name) : rows.OrderBy(r => r.Name),
            _ => desc ? rows.OrderByDescending(r => r.Price) : rows.OrderBy(r => r.Price)
        };

        // Page (A4): keep the page number inside 1..totalPages
        int total = rows.Count;
        int totalPages = (int)Math.Ceiling(total / (double)AdvancedSearchViewModel.PageSize);
        if (c.Page < 1) c.Page = 1;
        if (totalPages > 0 && c.Page > totalPages) c.Page = totalPages;

        var model = new AdvancedSearchViewModel
        {
            Criteria = c,
            Locations = await _db.Locations.OrderBy(l => l.City).ToListAsync(),
            Rows = sorted.Skip((c.Page - 1) * AdvancedSearchViewModel.PageSize).Take(AdvancedSearchViewModel.PageSize).ToList(),
            Total = total,
            TotalPages = totalPages
        };
        return View(model);
    }

    // ------------------------------------------------------------------
    // Query builders: one per type. Each adds a WHERE only for the
    // criteria the visitor filled in (Algorithms A2 and A3).
    // ------------------------------------------------------------------

    /// <summary>Fixes input that would give confusing results (A3 first steps).</summary>
    private static void Normalise(SearchCriteria c)
    {
        // The visitor typed the prices the wrong way round: swap them
        if (c.MinPrice.HasValue && c.MaxPrice.HasValue && c.MinPrice > c.MaxPrice)
            (c.MinPrice, c.MaxPrice) = (c.MaxPrice, c.MinPrice);
        if (c.Quantity < 1) c.Quantity = 1;
        c.Keyword = string.IsNullOrWhiteSpace(c.Keyword) ? null : c.Keyword.Trim();
        string[] tabs = { "Spot", "Hotel", "Restaurant", "Resort" };
        if (!tabs.Contains(c.Type)) c.Type = "Hotel";
    }

    /// <summary>A2: tourist spots by category, location, entry fee and keyword.</summary>
    private IQueryable<TouristSpot> SpotQuery(SearchCriteria c)
    {
        var q = _db.TouristSpots.Include(s => s.Location).AsQueryable();
        if (!string.IsNullOrEmpty(c.Category)) q = q.Where(s => s.Category == c.Category);
        if (c.LocationId.HasValue) q = q.Where(s => s.LocationId == c.LocationId);
        if (c.MinPrice.HasValue) q = q.Where(s => s.EntryFee >= c.MinPrice);
        if (c.MaxPrice.HasValue) q = q.Where(s => s.EntryFee <= c.MaxPrice);
        if (c.Keyword != null) q = q.Where(s => s.Name.Contains(c.Keyword) || (s.Description != null && s.Description.Contains(c.Keyword)));
        return q.OrderByDescending(s => s.IsFeatured).ThenBy(s => s.Name);
    }

    /// <summary>A3 for hotels: location, price per night, stars, rooms needed, availability.</summary>
    private IQueryable<Hotel> HotelQuery(SearchCriteria c)
    {
        var q = _db.Hotels.Include(h => h.Location).AsQueryable();
        if (c.LocationId.HasValue) q = q.Where(h => h.LocationId == c.LocationId);
        if (c.MinPrice.HasValue) q = q.Where(h => h.PricePerNight >= c.MinPrice);
        if (c.MaxPrice.HasValue) q = q.Where(h => h.PricePerNight <= c.MaxPrice);
        if (c.MinQuality.HasValue) q = q.Where(h => h.Stars >= c.MinQuality);
        if (c.OnlyAvailable) q = q.Where(h => h.RoomsAvailable >= c.Quantity);
        if (c.Keyword != null) q = q.Where(h => h.Name.Contains(c.Keyword));
        return q;
    }

    /// <summary>A3 for restaurants: location, price per person, rating, seats needed, availability.</summary>
    private IQueryable<Restaurant> RestaurantQuery(SearchCriteria c)
    {
        var q = _db.Restaurants.Include(r => r.Location).AsQueryable();
        if (c.LocationId.HasValue) q = q.Where(r => r.LocationId == c.LocationId);
        if (c.MinPrice.HasValue) q = q.Where(r => r.AvgPricePerPerson >= c.MinPrice);
        if (c.MaxPrice.HasValue) q = q.Where(r => r.AvgPricePerPerson <= c.MaxPrice);
        if (c.MinQuality.HasValue) q = q.Where(r => r.Rating >= c.MinQuality);
        if (c.OnlyAvailable) q = q.Where(r => r.SeatsAvailable >= c.Quantity);
        if (c.Keyword != null) q = q.Where(r => r.Name.Contains(c.Keyword) || r.Cuisine.Contains(c.Keyword));
        return q;
    }

    /// <summary>A3 for resorts: location, price per night, stars, villas needed, availability.</summary>
    private IQueryable<Resort> ResortQuery(SearchCriteria c)
    {
        var q = _db.Resorts.Include(r => r.Location).AsQueryable();
        if (c.LocationId.HasValue) q = q.Where(r => r.LocationId == c.LocationId);
        if (c.MinPrice.HasValue) q = q.Where(r => r.PricePerNight >= c.MinPrice);
        if (c.MaxPrice.HasValue) q = q.Where(r => r.PricePerNight <= c.MaxPrice);
        if (c.MinQuality.HasValue) q = q.Where(r => r.Stars >= c.MinQuality);
        if (c.OnlyAvailable) q = q.Where(r => r.UnitsAvailable >= c.Quantity);
        if (c.Keyword != null) q = q.Where(r => r.Name.Contains(c.Keyword));
        return q;
    }
}
