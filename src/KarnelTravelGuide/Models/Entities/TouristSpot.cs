using System.ComponentModel.DataAnnotations;

namespace KarnelTravelGuide.Models.Entities;

/// <summary>
/// A tourist spot such as a beach, monument or mountain (Tourist Spots page).
/// Maps to table dbo.TouristSpots.
/// </summary>
public class TouristSpot
{
    /// <summary>Allowed values of <see cref="Category"/>; must match the CHECK constraint in SQL.</summary>
    public static readonly string[] Categories =
        { "Beach", "Monument", "Mountain", "Nature", "Temple", "Cultural", "Island" };

    public int SpotId { get; set; }

    [Display(Name = "Location"), Range(1, int.MaxValue, ErrorMessage = "Please choose a location.")]
    public int LocationId { get; set; }

    [Required(ErrorMessage = "Please enter the name."), StringLength(150)]
    public string Name { get; set; } = "";

    [Required(ErrorMessage = "Please choose a category."), StringLength(30)]
    public string Category { get; set; } = "";

    [StringLength(1000)]
    public string? Description { get; set; }

    [StringLength(250)]
    public string? Address { get; set; }

    [Display(Name = "Entry fee (VND)"), Range(0, 100_000_000)]
    public decimal EntryFee { get; set; }

    [Display(Name = "Best time to visit"), StringLength(100)]
    public string? BestTime { get; set; }

    [Display(Name = "Image URL"), StringLength(250)]
    public string? ImageUrl { get; set; }

    /// <summary>Featured spots appear on the Home Page.</summary>
    [Display(Name = "Featured on Home Page")]
    public bool IsFeatured { get; set; }

    public DateTime CreatedAt { get; set; }

    public Location? Location { get; set; }
    public ICollection<PackageSpot> PackageSpots { get; set; } = new List<PackageSpot>();
}
