using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarnelTravelGuide.Models.Entities;

/// <summary>
/// A tour package or discount offer (Home and Information pages).
/// Maps to table dbo.Packages.
/// </summary>
public class Package
{
    public int PackageId { get; set; }

    [Display(Name = "Main location"), Range(1, int.MaxValue, ErrorMessage = "Please choose a location.")]
    public int LocationId { get; set; }

    [Required(ErrorMessage = "Please enter the title."), StringLength(150)]
    public string Title { get; set; } = "";

    [StringLength(1000)]
    public string? Description { get; set; }

    [Display(Name = "Days"), Range(1, 30)]
    public byte DurationDays { get; set; } = 1;

    [Display(Name = "Price (VND)"), Range(1, 1_000_000_000, ErrorMessage = "Price must be greater than 0.")]
    public decimal Price { get; set; }

    [Display(Name = "Discount (%)"), Range(0, 90)]
    public byte DiscountPercent { get; set; }

    [Display(Name = "Valid from"), DataType(DataType.Date)]
    public DateTime ValidFrom { get; set; } = DateTime.Today;

    [Display(Name = "Valid until"), DataType(DataType.Date)]
    public DateTime ValidUntil { get; set; } = DateTime.Today.AddMonths(3);

    [Display(Name = "Image URL"), StringLength(250)]
    public string? ImageUrl { get; set; }

    [Display(Name = "Active")]
    public bool IsActive { get; set; } = true;

    /// <summary>Price after discount: Price × (100 − DiscountPercent) / 100 (Algorithm A1). Not stored.</summary>
    [NotMapped]
    public decimal FinalPrice => Math.Round(Price * (100 - DiscountPercent) / 100m, 0);

    public Location? Location { get; set; }
    public ICollection<PackageSpot> PackageSpots { get; set; } = new List<PackageSpot>();
}

/// <summary>
/// Link table: which tourist spots a package visits and on which day.
/// Maps to table dbo.PackageSpots (composite key PackageId + SpotId).
/// </summary>
public class PackageSpot
{
    public int PackageId { get; set; }
    public int SpotId { get; set; }
    public byte DayNumber { get; set; } = 1;

    public Package? Package { get; set; }
    public TouristSpot? Spot { get; set; }
}
