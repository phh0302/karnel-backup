using KarnelTravelGuide.Data;
using KarnelTravelGuide.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace KarnelTravelGuide.Areas.Admin.Controllers;

// One small controller per table. The shared logic lives in CrudController<T>.

/// <summary>/Admin/TouristSpots</summary>
public class TouristSpotsController : CrudController<TouristSpot>
{
    public TouristSpotsController(KarnelDbContext db) : base(db) { }
    protected override string Kind => "tourist spot";
    protected override string NameOf(TouristSpot x) => x.Name;
    protected override IQueryable<TouristSpot> ListQuery() =>
        Db.TouristSpots.Include(x => x.Location).OrderBy(x => x.Location!.City).ThenBy(x => x.Name);

    protected override void ValidateExtra(TouristSpot x)
    {
        if (!TouristSpot.Categories.Contains(x.Category))
            ModelState.AddModelError(nameof(x.Category), "Please choose a category from the list.");
    }
}

/// <summary>/Admin/Hotels</summary>
public class HotelsController : CrudController<Hotel>
{
    public HotelsController(KarnelDbContext db) : base(db) { }
    protected override string Kind => "hotel";
    protected override string NameOf(Hotel x) => x.Name;
    protected override IQueryable<Hotel> ListQuery() =>
        Db.Hotels.Include(x => x.Location).OrderBy(x => x.Location!.City).ThenBy(x => x.Name);

    protected override void ValidateExtra(Hotel x)
    {
        if (x.RoomsAvailable > x.TotalRooms)
            ModelState.AddModelError(nameof(x.RoomsAvailable), "Rooms available cannot be more than total rooms.");
    }
}

/// <summary>/Admin/Restaurants</summary>
public class RestaurantsController : CrudController<Restaurant>
{
    public RestaurantsController(KarnelDbContext db) : base(db) { }
    protected override string Kind => "restaurant";
    protected override string NameOf(Restaurant x) => x.Name;
    protected override IQueryable<Restaurant> ListQuery() =>
        Db.Restaurants.Include(x => x.Location).OrderBy(x => x.Location!.City).ThenBy(x => x.Name);

    protected override void ValidateExtra(Restaurant x)
    {
        if (x.SeatsAvailable > x.TotalSeats)
            ModelState.AddModelError(nameof(x.SeatsAvailable), "Seats available cannot be more than total seats.");
    }
}

/// <summary>/Admin/Resorts</summary>
public class ResortsController : CrudController<Resort>
{
    public ResortsController(KarnelDbContext db) : base(db) { }
    protected override string Kind => "resort";
    protected override string NameOf(Resort x) => x.Name;
    protected override IQueryable<Resort> ListQuery() =>
        Db.Resorts.Include(x => x.Location).OrderBy(x => x.Location!.City).ThenBy(x => x.Name);

    protected override void ValidateExtra(Resort x)
    {
        if (x.UnitsAvailable > x.TotalUnits)
            ModelState.AddModelError(nameof(x.UnitsAvailable), "Units available cannot be more than total units.");
    }
}

/// <summary>/Admin/Transports</summary>
public class TransportsController : CrudController<Transport>
{
    public TransportsController(KarnelDbContext db) : base(db) { }
    protected override string Kind => "route";
    protected override string NameOf(Transport x) => $"{x.TransportType} #{x.TransportId}";
    protected override IQueryable<Transport> ListQuery() =>
        Db.Transports.Include(x => x.FromLocation).Include(x => x.ToLocation)
            .OrderBy(x => x.FromLocation!.City).ThenBy(x => x.ToLocation!.City);

    protected override void ValidateExtra(Transport x)
    {
        if (x.FromLocationId == x.ToLocationId)
            ModelState.AddModelError(nameof(x.ToLocationId), "The route must end in a different city.");
        if (!Transport.Types.Contains(x.TransportType))
            ModelState.AddModelError(nameof(x.TransportType), "Please choose a type from the list.");
    }
}

/// <summary>/Admin/Packages</summary>
public class PackagesController : CrudController<Package>
{
    public PackagesController(KarnelDbContext db) : base(db) { }
    protected override string Kind => "package";
    protected override string NameOf(Package x) => x.Title;
    protected override IQueryable<Package> ListQuery() =>
        Db.Packages.Include(x => x.Location).OrderByDescending(x => x.IsActive).ThenBy(x => x.ValidUntil);

    protected override void ValidateExtra(Package x)
    {
        if (x.ValidUntil < x.ValidFrom)
            ModelState.AddModelError(nameof(x.ValidUntil), "Valid until must be on or after valid from.");
    }
}
