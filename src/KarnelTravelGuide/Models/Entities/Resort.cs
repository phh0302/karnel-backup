using System.ComponentModel.DataAnnotations;

namespace KarnelTravelGuide.Models.Entities;

/// <summary>
/// A resort (Resorts Information page and resort search).
/// Units are villas or bungalows; UnitsAvailable is the quantity filter.
/// Maps to table dbo.Resorts.
/// </summary>
public class Resort
{
    public int ResortId { get; set; }

    [Display(Name = "Location"), Range(1, int.MaxValue, ErrorMessage = "Please choose a location.")]
    public int LocationId { get; set; }

    [Required(ErrorMessage = "Please enter the name."), StringLength(150)]
    public string Name { get; set; } = "";

    [Range(1, 5, ErrorMessage = "Stars must be from 1 to 5.")]
    public byte Stars { get; set; } = 4;

    [StringLength(250)]
    public string? Address { get; set; }

    [StringLength(20)]
    public string? Phone { get; set; }

    [Display(Name = "Price per night (VND)"), Range(1, 1_000_000_000, ErrorMessage = "Price must be greater than 0.")]
    public decimal PricePerNight { get; set; }

    [Display(Name = "Total villas / units"), Range(1, 10_000)]
    public int TotalUnits { get; set; }

    [Display(Name = "Units available"), Range(0, 10_000)]
    public int UnitsAvailable { get; set; }

    [Range(0, 5)]
    public decimal? Rating { get; set; }

    [StringLength(300)]
    public string? Facilities { get; set; }

    [StringLength(1000)]
    public string? Description { get; set; }

    [Display(Name = "Image URL"), StringLength(250)]
    public string? ImageUrl { get; set; }

    [Display(Name = "Mark as new")]
    public bool IsNew { get; set; }

    public DateTime CreatedAt { get; set; }

    public Location? Location { get; set; }
}
