using System.ComponentModel.DataAnnotations;

namespace KarnelTravelGuide.Models.Entities;

/// <summary>
/// A hotel (Hotel Information page and hotel search).
/// RoomsAvailable drives the "quantity" and "availability" search filters.
/// Maps to table dbo.Hotels.
/// </summary>
public class Hotel
{
    public int HotelId { get; set; }

    [Display(Name = "Location"), Range(1, int.MaxValue, ErrorMessage = "Please choose a location.")]
    public int LocationId { get; set; }

    [Required(ErrorMessage = "Please enter the name."), StringLength(150)]
    public string Name { get; set; } = "";

    [Range(1, 5, ErrorMessage = "Stars must be from 1 to 5.")]
    public byte Stars { get; set; } = 3;

    [StringLength(250)]
    public string? Address { get; set; }

    [StringLength(20)]
    public string? Phone { get; set; }

    [Display(Name = "Price per night (VND)"), Range(1, 1_000_000_000, ErrorMessage = "Price must be greater than 0.")]
    public decimal PricePerNight { get; set; }

    [Display(Name = "Total rooms"), Range(1, 10_000)]
    public int TotalRooms { get; set; }

    [Display(Name = "Rooms available"), Range(0, 10_000)]
    public int RoomsAvailable { get; set; }

    [Range(0, 5)]
    public decimal? Rating { get; set; }

    [StringLength(1000)]
    public string? Description { get; set; }

    [Display(Name = "Image URL"), StringLength(250)]
    public string? ImageUrl { get; set; }

    /// <summary>New hotels are listed in the "New on Karnel" block of the Information page.</summary>
    [Display(Name = "Mark as new")]
    public bool IsNew { get; set; }

    public DateTime CreatedAt { get; set; }

    public Location? Location { get; set; }
}
