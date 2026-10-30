using System.ComponentModel.DataAnnotations;
using KarnelTravelGuide.Models.Entities;

namespace KarnelTravelGuide.Areas.Admin.Models;

/// <summary>Admin login form (Algorithm A7).</summary>
public class LoginForm
{
    [Required(ErrorMessage = "Please enter username and password.")]
    public string Username { get; set; } = "";

    [Required(ErrorMessage = "Please enter username and password."), DataType(DataType.Password)]
    public string Password { get; set; } = "";

    /// <summary>Page to return to after logging in.</summary>
    public string? ReturnUrl { get; set; }
}

/// <summary>Dashboard figures.</summary>
public class DashboardViewModel
{
    public int NewBookings { get; set; }
    public int UnreadFeedback { get; set; }
    public int Spots { get; set; }
    public int Hotels { get; set; }
    public int Restaurants { get; set; }
    public int Resorts { get; set; }
    public int Routes { get; set; }
    public int Packages { get; set; }
    public List<BookingRow> LatestBookings { get; set; } = new();
}

/// <summary>A booking with the customer and the readable name of the booked service.</summary>
public class BookingRow
{
    public Booking Booking { get; set; } = null!;
    public string ServiceName { get; set; } = "";
}

/// <summary>Bookings list with an optional status filter.</summary>
public class BookingsViewModel
{
    public List<BookingRow> Rows { get; set; } = new();
    public string? Status { get; set; }
    public Dictionary<string, int> Counts { get; set; } = new();
}

/// <summary>Shared delete confirmation page.</summary>
public class DeleteViewModel
{
    public int Id { get; set; }
    public string Kind { get; set; } = "";     // e.g. "hotel"
    public string Name { get; set; } = "";
}
