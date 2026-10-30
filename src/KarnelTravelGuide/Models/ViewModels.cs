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

/// <summary>Information Page: links to the 5 sections, new places and packages (CRS FR-40 to FR-42).</summary>
public class InformationIndexViewModel
{
    public List<PlaceCard> NewHotels { get; set; } = new();
    public List<PlaceCard> NewRestaurants { get; set; } = new();
    public List<PlaceCard> NewResorts { get; set; } = new();
    public List<PlaceCard> Packages { get; set; } = new();
    public int SpotCount { get; set; }
    public int RouteCount { get; set; }
    public int HotelCount { get; set; }
    public int RestaurantCount { get; set; }
    public int ResortCount { get; set; }
}

/// <summary>An Information sub-page that lists places as cards with a city filter.</summary>
public class PlaceListViewModel
{
    public string Title { get; set; } = "";
    public string Intro { get; set; } = "";
    public string Action { get; set; } = "";               // action name for the filter form
    public List<PlaceCard> Cards { get; set; } = new();
    public List<Location> Locations { get; set; } = new();
    public int? LocationId { get; set; }
    public string[]? Categories { get; set; }             // only for tourist spots
    public string? Category { get; set; }
}

/// <summary>Travel Information page: transport routes with filters (CRS FR-44).</summary>
public class TravelViewModel
{
    public List<Transport> Routes { get; set; } = new();
    public List<Location> Locations { get; set; } = new();
    public int? FromId { get; set; }
    public int? ToId { get; set; }
    public string? Type { get; set; }
}

/// <summary>One detail page layout shared by spots, hotels, restaurants, resorts and packages.</summary>
public class DetailViewModel
{
    public string Section { get; set; } = "";             // e.g. "Hotels" (breadcrumb text)
    public string SectionAction { get; set; } = "";       // e.g. "Hotels" (breadcrumb link)
    public string Title { get; set; } = "";
    public string Subtitle { get; set; } = "";
    public string? ImageUrl { get; set; }
    public string? Description { get; set; }
    public int Stars { get; set; }
    public string? Badge { get; set; }
    public List<(string Label, string Value)> Facts { get; set; } = new();
    public List<PlaceCard> Related { get; set; } = new();  // spots in a package, places nearby
    public string RelatedTitle { get; set; } = "";
    public string? BookServiceType { get; set; }           // null = nothing to book (tourist spot)
    public int BookServiceId { get; set; }
}

/// <summary>
/// Search criteria shared by the Search and Advanced Search pages (Algorithms A2, A3, A4).
/// Bound from the query string, so a search can be bookmarked or shared.
/// </summary>
public class SearchCriteria
{
    public string Type { get; set; } = "Hotel";            // Spot, Hotel, Restaurant, Resort
    public int? LocationId { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public decimal? MinQuality { get; set; }              // stars for hotels/resorts, rating for restaurants
    public int Quantity { get; set; } = 1;                // rooms, seats or villas needed
    public bool OnlyAvailable { get; set; } = true;
    public string? Category { get; set; }                 // tourist spot type
    public string? Keyword { get; set; }

    // Advanced search only
    public List<string> Types { get; set; } = new();
    public string SortBy { get; set; } = "price";         // price, quality, name
    public string SortDir { get; set; } = "asc";          // asc, desc
    public int Page { get; set; } = 1;
}

/// <summary>One result row for Advanced Search.</summary>
public class SearchRow
{
    public string Type { get; set; } = "";
    public string Name { get; set; } = "";
    public string City { get; set; } = "";
    public decimal Price { get; set; }
    public string PriceUnit { get; set; } = "";
    public decimal Quality { get; set; }                  // for sorting
    public string QualityText { get; set; } = "";
    public string Url { get; set; } = "";
}

/// <summary>Search page: criteria, the list of cities, and result cards.</summary>
public class SearchViewModel
{
    public SearchCriteria Criteria { get; set; } = new();
    public List<Location> Locations { get; set; } = new();
    public List<PlaceCard> Results { get; set; } = new();
}

/// <summary>Advanced Search page: criteria and one page of sorted results.</summary>
public class AdvancedSearchViewModel
{
    public SearchCriteria Criteria { get; set; } = new();
    public List<Location> Locations { get; set; } = new();
    public List<SearchRow> Rows { get; set; } = new();
    public int Total { get; set; }
    public int TotalPages { get; set; }
    public const int PageSize = 10;
}
