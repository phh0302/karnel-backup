using System.ComponentModel.DataAnnotations;

namespace KarnelTravelGuide.Models.Entities;

/// <summary>
/// A transport route between two locations (Travel Information page).
/// Maps to table dbo.Transports.
/// </summary>
public class Transport
{
    /// <summary>Allowed values of <see cref="TransportType"/>; must match the CHECK constraint in SQL.</summary>
    public static readonly string[] Types = { "Bus", "Train", "Car", "Plane", "Boat" };

    public int TransportId { get; set; }

    [Display(Name = "From"), Range(1, int.MaxValue, ErrorMessage = "Please choose where the route starts.")]
    public int FromLocationId { get; set; }

    [Display(Name = "To"), Range(1, int.MaxValue, ErrorMessage = "Please choose where the route ends.")]
    public int ToLocationId { get; set; }

    [Display(Name = "Type"), Required(ErrorMessage = "Please choose a type."), StringLength(20)]
    public string TransportType { get; set; } = "";

    [Required(ErrorMessage = "Please enter the operator."), StringLength(100)]
    public string Operator { get; set; } = "";

    [Display(Name = "Fare (VND)"), Range(1, 100_000_000, ErrorMessage = "Fare must be greater than 0.")]
    public decimal Fare { get; set; }

    [Display(Name = "Duration (hours)"), Range(0.1, 100)]
    public decimal DurationHours { get; set; }

    [Display(Name = "Departure times"), StringLength(150)]
    public string? DepartureTimes { get; set; }

    [Display(Name = "Seats available"), Range(0, 10_000)]
    public int SeatsAvailable { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    public Location? FromLocation { get; set; }
    public Location? ToLocation { get; set; }
}
