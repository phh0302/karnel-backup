using System.ComponentModel.DataAnnotations;

namespace KarnelTravelGuide.Models.Entities;

/// <summary>
/// A restaurant (Restaurant Information page and restaurant search).
/// PriceLevel: 1 = budget ... 4 = fine dining. SeatsAvailable is the quantity filter.
/// Maps to table dbo.Restaurants.
/// </summary>
public class Restaurant
{
    public int RestaurantId { get; set; }

    [Display(Name = "Location"), Range(1, int.MaxValue, ErrorMessage = "Please choose a location.")]
    public int LocationId { get; set; }

    [Required(ErrorMessage = "Please enter the name."), StringLength(150)]
    public string Name { get; set; } = "";

    [Required(ErrorMessage = "Please enter the cuisine."), StringLength(50)]
    public string Cuisine { get; set; } = "";

    [StringLength(250)]
    public string? Address { get; set; }

    [StringLength(20)]
    public string? Phone { get; set; }

    [Display(Name = "Price level (1-4)"), Range(1, 4)]
    public byte PriceLevel { get; set; } = 2;

    [Display(Name = "Average price per person (VND)"), Range(1, 100_000_000, ErrorMessage = "Price must be greater than 0.")]
    public decimal AvgPricePerPerson { get; set; }

    [Display(Name = "Total seats"), Range(1, 10_000)]
    public int TotalSeats { get; set; }

    [Display(Name = "Seats available"), Range(0, 10_000)]
    public int SeatsAvailable { get; set; }

    [Range(0, 5)]
    public decimal? Rating { get; set; }

    [Display(Name = "Opening hours"), StringLength(50)]
    public string? OpenHours { get; set; }

    [StringLength(1000)]
    public string? Description { get; set; }

    [Display(Name = "Image URL"), StringLength(250)]
    public string? ImageUrl { get; set; }

    [Display(Name = "Mark as new")]
    public bool IsNew { get; set; }

    public DateTime CreatedAt { get; set; }

    public Location? Location { get; set; }
}
