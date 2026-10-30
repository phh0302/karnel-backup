using KarnelTravelGuide.Data;
using KarnelTravelGuide.Helpers;
using KarnelTravelGuide.Models;
using KarnelTravelGuide.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KarnelTravelGuide.Controllers;

/// <summary>
/// Contact Us: place an order (booking) and send feedback (CRS FR-50 to FR-54).
/// Follows Algorithms A5 (booking) and A6 (feedback).
/// </summary>
public class ContactController : Controller
{
    private readonly KarnelDbContext _db;

    public ContactController(KarnelDbContext db) => _db = db;

    // GET /Contact?serviceType=Hotel&serviceId=7
    // The optional parameters come from the "Book" buttons and pre-fill the form.
    public async Task<IActionResult> Index(string? serviceType, int? serviceId)
    {
        var model = new ContactViewModel
        {
            Services = await LoadServices(),
            Booking = new BookingForm
            {
                ServiceType = serviceType ?? "",
                ServiceId = serviceId ?? 0,
                TravelDate = DateTime.Today.AddDays(7)
            }
        };
        return View(model);
    }

    // POST /Contact/Book  (Algorithm A5)
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Book([Bind(Prefix = "Booking")] BookingForm form)
    {
        // ---- Step 1: rules the attributes cannot express ----
        if (form.TravelDate.HasValue && form.TravelDate.Value.Date < DateTime.Today)
            ModelState.AddModelError("Booking.TravelDate", "Travel date cannot be in the past.");
        if (!Booking.ServiceTypes.Contains(form.ServiceType))
            ModelState.AddModelError("Booking.ServiceType", "Please choose a service type.");

        // ---- Steps 2 and 3: the service must exist and have room ----
        if (ModelState.IsValid)
        {
            string? problem = await CheckService(form);
            if (problem != null) ModelState.AddModelError("Booking.ServiceId", problem);
        }

        if (!ModelState.IsValid)
        {
            // Show the form again with the errors and what the visitor typed
            return View("Index", new ContactViewModel { Booking = form, Services = await LoadServices() });
        }

        // ---- Step 4: find or create the customer by email ----
        var customer = await FindOrCreateCustomer(form.FullName, form.Email, form.Phone);

        // ---- Step 5: save the booking with status New; staff confirm it later (A8) ----
        var booking = new Booking
        {
            CustomerId = customer.CustomerId,
            ServiceType = form.ServiceType,
            ServiceId = form.ServiceId,
            TravelDate = form.TravelDate!.Value.Date,
            NumberOfPeople = form.NumberOfPeople,
            Quantity = form.Quantity,
            Note = string.IsNullOrWhiteSpace(form.Note) ? null : form.Note.Trim(),
            Status = "New"
        };
        _db.Bookings.Add(booking);
        await _db.SaveChangesAsync();

        // Post-Redirect-Get: refreshing the thank-you page does not send the booking twice
        TempData["Thanks"] = $"Thank you, {customer.FullName}! Your booking number is #{booking.BookingId}. We will call you within 24 hours to confirm.";
        return RedirectToAction(nameof(Thanks));
    }

    // POST /Contact/Feedback  (Algorithm A6)
    // Named SendFeedback so it does not hide the Feedback entity class; the URL stays /Contact/Feedback.
    [HttpPost, ValidateAntiForgeryToken, ActionName("Feedback")]
    public async Task<IActionResult> SendFeedback([Bind(Prefix = "Feedback")] FeedbackForm form)
    {
        if (!ModelState.IsValid)
        {
            return View("Index", new ContactViewModel
            {
                Feedback = form,
                Services = await LoadServices(),
                Booking = new BookingForm { TravelDate = DateTime.Today.AddDays(7) },
                OpenTab = "feedback"
            });
        }

        var customer = await FindOrCreateCustomer(form.FullName, form.Email, form.Phone);
        _db.Feedbacks.Add(new Feedback
        {
            CustomerId = customer.CustomerId,
            Rating = form.Rating,
            Subject = string.IsNullOrWhiteSpace(form.Subject) ? null : form.Subject.Trim(),
            Message = form.Message.Trim(),
            IsRead = false
        });
        await _db.SaveChangesAsync();

        TempData["Thanks"] = $"Thank you for your feedback, {customer.FullName}. It helps us improve Karnel Travels.";
        return RedirectToAction(nameof(Thanks));
    }

    // GET /Contact/Thanks
    public IActionResult Thanks()
    {
        // Opened directly without a message: nothing to confirm, go back to the form
        if (TempData["Thanks"] is not string message) return RedirectToAction(nameof(Index));
        ViewBag.Message = message;
        return View();
    }

    // ------------------------------------------------------------------
    // Helpers
    // ------------------------------------------------------------------

    /// <summary>Builds the list for the "Service" drop-down: every bookable item with its type.</summary>
    private async Task<List<ServiceOption>> LoadServices()
    {
        var today = DateTime.Today;
        var list = new List<ServiceOption>();
        list.AddRange(await _db.Hotels.OrderBy(x => x.Name)
            .Select(x => new ServiceOption("Hotel", x.HotelId, x.Name + " (" + x.Location!.City + ")")).ToListAsync());
        list.AddRange(await _db.Restaurants.OrderBy(x => x.Name)
            .Select(x => new ServiceOption("Restaurant", x.RestaurantId, x.Name + " (" + x.Location!.City + ")")).ToListAsync());
        list.AddRange(await _db.Resorts.OrderBy(x => x.Name)
            .Select(x => new ServiceOption("Resort", x.ResortId, x.Name + " (" + x.Location!.City + ")")).ToListAsync());
        list.AddRange(await _db.Transports.OrderBy(x => x.FromLocation!.City)
            .Select(x => new ServiceOption("Transport", x.TransportId,
                x.FromLocation!.City + " → " + x.ToLocation!.City + " · " + x.TransportType + " · " + x.Operator)).ToListAsync());
        list.AddRange(await _db.Packages.Where(p => p.IsActive && p.ValidUntil >= today).OrderBy(x => x.Title)
            .Select(x => new ServiceOption("Package", x.PackageId, x.Title)).ToListAsync());
        return list;
    }

    /// <summary>
    /// A5 steps 2 and 3: returns an error message if the chosen service does not exist
    /// or cannot take this booking, otherwise null.
    /// </summary>
    private async Task<string?> CheckService(BookingForm f)
    {
        const string gone = "This service is no longer available. Please choose another one.";
        switch (f.ServiceType)
        {
            case "Hotel":
                var h = await _db.Hotels.FindAsync(f.ServiceId);
                if (h == null) return gone;
                return h.RoomsAvailable < f.Quantity ? $"Only {h.RoomsAvailable} rooms are left at {h.Name}." : null;
            case "Resort":
                var r = await _db.Resorts.FindAsync(f.ServiceId);
                if (r == null) return gone;
                return r.UnitsAvailable < f.Quantity ? $"Only {r.UnitsAvailable} villas are left at {r.Name}." : null;
            case "Restaurant":
                var rs = await _db.Restaurants.FindAsync(f.ServiceId);
                if (rs == null) return gone;
                return rs.SeatsAvailable < f.NumberOfPeople ? $"Only {rs.SeatsAvailable} seats are left at {rs.Name}." : null;
            case "Transport":
                var t = await _db.Transports.FindAsync(f.ServiceId);
                if (t == null) return gone;
                return t.SeatsAvailable < f.NumberOfPeople ? $"Only {t.SeatsAvailable} seats are left on this route." : null;
            case "Package":
                var p = await _db.Packages.FindAsync(f.ServiceId);
                if (p == null) return gone;
                var day = f.TravelDate!.Value.Date;
                return p.IsActive && day >= p.ValidFrom && day <= p.ValidUntil
                    ? null
                    : $"This package runs from {p.ValidFrom:dd/MM/yyyy} to {p.ValidUntil:dd/MM/yyyy}. Please choose a date in that period.";
            default:
                return "Please choose a service type.";
        }
    }

    /// <summary>A5 step 4: the email identifies a returning customer; new emails create a customer.</summary>
    private async Task<Customer> FindOrCreateCustomer(string fullName, string email, string phone)
    {
        string key = email.Trim().ToLowerInvariant();
        var customer = await _db.Customers.FirstOrDefaultAsync(c => c.Email == key);
        if (customer == null)
        {
            customer = new Customer { FullName = fullName.Trim(), Email = key, Phone = phone.Trim() };
            _db.Customers.Add(customer);
        }
        else
        {
            // Keep the latest name and phone the customer gave us
            customer.FullName = fullName.Trim();
            customer.Phone = phone.Trim();
        }
        await _db.SaveChangesAsync();
        return customer;
    }
}
