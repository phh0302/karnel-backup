using System.ComponentModel.DataAnnotations;

namespace KarnelTravelGuide.Models.Entities;

/// <summary>
/// A city or town in Viet Nam. Every place on the site belongs to one location,
/// which is what lets visitors search and filter by location.
/// Maps to table dbo.Locations.
/// </summary>
public class Location
{
    public int LocationId { get; set; }

    [Required, StringLength(100)]
    public string City { get; set; } = "";

    [Required, StringLength(100)]
    public string Province { get; set; } = "";

    /// <summary>North, Central or South.</summary>
    [Required, StringLength(20)]
    public string Region { get; set; } = "";

    [StringLength(500)]
    public string? Description { get; set; }

    // Navigation collections (one location has many places)
    public ICollection<TouristSpot> TouristSpots { get; set; } = new List<TouristSpot>();
    public ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();
    public ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
    public ICollection<Resort> Resorts { get; set; } = new List<Resort>();
    public ICollection<Package> Packages { get; set; } = new List<Package>();
}
