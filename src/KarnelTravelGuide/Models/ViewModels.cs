using KarnelTravelGuide.Models.Entities;

namespace KarnelTravelGuide.Models;

/// <summary>
/// Data for one card in a grid (used by Home, Information and Search pages).
/// Built by the controllers so every card looks the same (GUI Standards, section 4).
/// </summary>
public class PlaceCard
{
    public string Title { get; set; } = "";
    public string Subtitle { get; set; } = "";      // e.g. city, category
    public string? ImageUrl { get; set; }
    public string Url { get; set; } = "#";
    public string? PriceText { get; set; }          // e.g. "1.800.000 ₫ / night"
    public string? OldPriceText { get; set; }       // shown struck through for discounts
    public string? Badge { get; set; }              // e.g. "New", "-15%"
    public int Stars { get; set; }                  // 0 = do not show stars
    public string? Extra { get; set; }              // e.g. "12 rooms left"
}

/// <summary>Home Page data.</summary>
public class HomeViewModel
{
    public List<PlaceCard> FeaturedSpots { get; set; } = new();
    public List<PlaceCard> Packages { get; set; } = new();
    public List<Location> Locations { get; set; } = new();
}

/// <summary>About Us page data: real counts from the database.</summary>
public class AboutViewModel
{
    public int Destinations { get; set; }
    public int Hotels { get; set; }
    public int Resorts { get; set; }
    public int Routes { get; set; }
    public int Packages { get; set; }
}
