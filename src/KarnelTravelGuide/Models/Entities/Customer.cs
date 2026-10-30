namespace KarnelTravelGuide.Models.Entities;

/// <summary>
/// A person who booked a service or sent feedback. Customers do not log in;
/// the email address identifies a returning customer (Algorithm A5, step 4).
/// Maps to table dbo.Customers.
/// </summary>
public class Customer
{
    public int CustomerId { get; set; }
    public string FullName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Phone { get; set; } = "";
    public DateTime CreatedAt { get; set; }

    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
}

/// <summary>
/// An order placed from the Contact Us page. ServiceType + ServiceId point to a row
/// in Hotels, Restaurants, Resorts, Transports or Packages.
/// Maps to table dbo.Bookings.
/// </summary>
public class Booking
{
    /// <summary>Allowed service types; must match the CHECK constraint in SQL.</summary>
    public static readonly string[] ServiceTypes = { "Hotel", "Restaurant", "Resort", "Transport", "Package" };

    /// <summary>Allowed statuses; must match the CHECK constraint in SQL.</summary>
    public static readonly string[] Statuses = { "New", "Contacted", "Completed", "Cancelled" };

    public int BookingId { get; set; }
    public int CustomerId { get; set; }
    public string ServiceType { get; set; } = "";
    public int ServiceId { get; set; }
    public DateTime TravelDate { get; set; }
    public int NumberOfPeople { get; set; }
    public int Quantity { get; set; } = 1;
    public string? Note { get; set; }
    public string Status { get; set; } = "New";
    public DateTime CreatedAt { get; set; }

    public Customer? Customer { get; set; }
}

/// <summary>
/// Feedback sent from the Contact Us page. Maps to table dbo.Feedbacks.
/// </summary>
public class Feedback
{
    public int FeedbackId { get; set; }
    public int CustomerId { get; set; }
    public byte Rating { get; set; }
    public string? Subject { get; set; }
    public string Message { get; set; } = "";
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; }

    public Customer? Customer { get; set; }
}

/// <summary>
/// A staff account for the admin area. PasswordHash is made by ASP.NET Core PasswordHasher.
/// Maps to table dbo.Admins.
/// </summary>
public class Admin
{
    public int AdminId { get; set; }
    public string Username { get; set; } = "";
    public string PasswordHash { get; set; } = "";
    public string FullName { get; set; } = "";
    public DateTime? LastLoginAt { get; set; }
    public DateTime CreatedAt { get; set; }
}
