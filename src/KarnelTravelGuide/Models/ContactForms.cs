using System.ComponentModel.DataAnnotations;

namespace KarnelTravelGuide.Models;

/// <summary>
/// Booking form on the Contact Us page (CRS FR-50, FR-51, FR-53).
/// The attributes give both browser-side and server-side validation;
/// messages follow the Interface Design Document.
/// </summary>
public class BookingForm
{
    [Display(Name = "Full name")]
    [Required(ErrorMessage = "Please enter your full name.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Please enter your full name.")]
    public string FullName { get; set; } = "";

    [Display(Name = "Email")]
    [Required(ErrorMessage = "Please enter a valid email address.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    [StringLength(150)]
    public string Email { get; set; } = "";

    [Display(Name = "Phone")]
    [Required(ErrorMessage = "Please enter a 10-digit phone number starting with 0.")]
    [RegularExpression(@"^0\d{9}$", ErrorMessage = "Please enter a 10-digit phone number starting with 0.")]
    public string Phone { get; set; } = "";

    [Display(Name = "Service type")]
    [Required(ErrorMessage = "Please choose a service type.")]
    public string ServiceType { get; set; } = "";

    [Display(Name = "Service")]
    [Range(1, int.MaxValue, ErrorMessage = "Please choose a service.")]
    public int ServiceId { get; set; }

    [Display(Name = "Travel date")]
    [Required(ErrorMessage = "Please choose a travel date.")]
    [DataType(DataType.Date)]
    public DateTime? TravelDate { get; set; }

    [Display(Name = "Number of people")]
    [Range(1, 50, ErrorMessage = "Number of people must be from 1 to 50.")]
    public int NumberOfPeople { get; set; } = 1;

    [Display(Name = "Rooms / seats / villas")]
    [Range(1, 50, ErrorMessage = "Quantity must be from 1 to 50.")]
    public int Quantity { get; set; } = 1;

    [Display(Name = "Note")]
    [StringLength(500, ErrorMessage = "Note can be at most 500 characters.")]
    public string? Note { get; set; }
}

/// <summary>Feedback form on the Contact Us page (CRS FR-52, FR-53).</summary>
public class FeedbackForm
{
    [Display(Name = "Full name")]
    [Required(ErrorMessage = "Please enter your full name.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Please enter your full name.")]
    public string FullName { get; set; } = "";

    [Display(Name = "Email")]
    [Required(ErrorMessage = "Please enter a valid email address.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    [StringLength(150)]
    public string Email { get; set; } = "";

    [Display(Name = "Phone")]
    [Required(ErrorMessage = "Please enter a 10-digit phone number starting with 0.")]
    [RegularExpression(@"^0\d{9}$", ErrorMessage = "Please enter a 10-digit phone number starting with 0.")]
    public string Phone { get; set; } = "";

    [Display(Name = "Rating")]
    [Range(1, 5, ErrorMessage = "Please choose a rating from 1 to 5 stars.")]
    public byte Rating { get; set; }

    [Display(Name = "Subject")]
    [StringLength(150, ErrorMessage = "Subject can be at most 150 characters.")]
    public string? Subject { get; set; }

    [Display(Name = "Message")]
    [Required(ErrorMessage = "Please write at least 10 characters.")]
    [StringLength(2000, MinimumLength = 10, ErrorMessage = "Please write between 10 and 2000 characters.")]
    public string Message { get; set; } = "";
}

/// <summary>One choice in the "Service" drop-down; Type lets the page filter by service type.</summary>
public record ServiceOption(string Type, int Id, string Label);

/// <summary>Contact Us page: both forms and the list of services that can be booked.</summary>
public class ContactViewModel
{
    public BookingForm Booking { get; set; } = new();
    public FeedbackForm Feedback { get; set; } = new();
    public List<ServiceOption> Services { get; set; } = new();
    public string? OpenTab { get; set; }      // "feedback" to scroll to the feedback form after an error
}
