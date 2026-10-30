using KarnelTravelGuide.Models;
using KarnelTravelGuide.Models.Entities;

namespace KarnelTravelGuide.Helpers;

/// <summary>
/// Turns database rows into <see cref="PlaceCard"/> objects so every page
/// shows places in the same way. Location must be loaded (Include) for the city name.
/// </summary>
public static class Cards
{
    public static PlaceCard From(TouristSpot s) => new()
    {
        Title = s.Name,
        Subtitle = $"{s.Category} · {s.Location?.City}",
        ImageUrl = s.ImageUrl,
        Url = $"/Information/Spot/{s.SpotId}",
        PriceText = s.EntryFee == 0 ? "Free entry" : Money.Vnd(s.EntryFee),
        Extra = s.BestTime
    };

    public static PlaceCard From(Hotel h) => new()
    {
        Title = h.Name,
        Subtitle = h.Location?.City ?? "",
        ImageUrl = h.ImageUrl,
        Url = $"/Information/Hotel/{h.HotelId}",
        Stars = h.Stars,
        PriceText = Money.Vnd(h.PricePerNight) + " / night",
        Badge = h.IsNew ? "New" : null,
        Extra = h.RoomsAvailable > 0 ? $"{h.RoomsAvailable} rooms left" : "Full"
    };

    public static PlaceCard From(Restaurant r) => new()
    {
        Title = r.Name,
        Subtitle = $"{r.Cuisine} · {r.Location?.City}",
        ImageUrl = r.ImageUrl,
        Url = $"/Information/Restaurant/{r.RestaurantId}",
        PriceText = "~" + Money.Vnd(r.AvgPricePerPerson) + " / person",
        Badge = r.IsNew ? "New" : null,
        Extra = r.Rating.HasValue ? $"Rated {r.Rating:0.0}" : null
    };

    public static PlaceCard From(Resort r) => new()
    {
        Title = r.Name,
        Subtitle = r.Location?.City ?? "",
        ImageUrl = r.ImageUrl,
        Url = $"/Information/Resort/{r.ResortId}",
        Stars = r.Stars,
        PriceText = Money.Vnd(r.PricePerNight) + " / night",
        Badge = r.IsNew ? "New" : null,
        Extra = r.UnitsAvailable > 0 ? $"{r.UnitsAvailable} villas left" : "Full"
    };

    public static PlaceCard From(Package p) => new()
    {
        Title = p.Title,
        Subtitle = $"{p.DurationDays} days · {p.Location?.City}",
        ImageUrl = p.ImageUrl,
        Url = $"/Information/Package/{p.PackageId}",
        PriceText = Money.Vnd(p.FinalPrice),
        OldPriceText = p.DiscountPercent > 0 ? Money.Vnd(p.Price) : null,
        Badge = p.DiscountPercent > 0 ? $"-{p.DiscountPercent}%" : null,
        Extra = $"Until {p.ValidUntil:dd/MM/yyyy}"
    };
}
