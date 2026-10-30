using KarnelTravelGuide.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace KarnelTravelGuide.Data;

/// <summary>
/// Entity Framework Core context for the KarnelTravelDB database.
/// The database is created by Database/01_create_schema.sql (database-first),
/// so this class only maps the C# classes to the existing tables; it does not use migrations.
/// </summary>
public class KarnelDbContext : DbContext
{
    public KarnelDbContext(DbContextOptions<KarnelDbContext> options) : base(options) { }

    public DbSet<Location> Locations => Set<Location>();
    public DbSet<TouristSpot> TouristSpots => Set<TouristSpot>();
    public DbSet<Hotel> Hotels => Set<Hotel>();
    public DbSet<Restaurant> Restaurants => Set<Restaurant>();
    public DbSet<Resort> Resorts => Set<Resort>();
    public DbSet<Transport> Transports => Set<Transport>();
    public DbSet<Package> Packages => Set<Package>();
    public DbSet<PackageSpot> PackageSpots => Set<PackageSpot>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<Feedback> Feedbacks => Set<Feedback>();
    public DbSet<Admin> Admins => Set<Admin>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        // ---- Table names (the SQL tables use plural names) ----
        b.Entity<Location>().ToTable("Locations");
        b.Entity<TouristSpot>().ToTable("TouristSpots").HasKey(x => x.SpotId);
        b.Entity<Hotel>().ToTable("Hotels");
        b.Entity<Restaurant>().ToTable("Restaurants");
        b.Entity<Resort>().ToTable("Resorts");
        b.Entity<Transport>().ToTable("Transports");
        b.Entity<Package>().ToTable("Packages");
        b.Entity<PackageSpot>().ToTable("PackageSpots").HasKey(x => new { x.PackageId, x.SpotId });
        b.Entity<Customer>().ToTable("Customers");
        b.Entity<Booking>().ToTable("Bookings");
        b.Entity<Feedback>().ToTable("Feedbacks");
        b.Entity<Admin>().ToTable("Admins");

        // ---- Money and rating columns: same precision as the SQL script ----
        b.Entity<TouristSpot>().Property(x => x.EntryFee).HasPrecision(12, 0);
        b.Entity<Hotel>().Property(x => x.PricePerNight).HasPrecision(12, 0);
        b.Entity<Hotel>().Property(x => x.Rating).HasPrecision(2, 1);
        b.Entity<Restaurant>().Property(x => x.AvgPricePerPerson).HasPrecision(12, 0);
        b.Entity<Restaurant>().Property(x => x.Rating).HasPrecision(2, 1);
        b.Entity<Resort>().Property(x => x.PricePerNight).HasPrecision(12, 0);
        b.Entity<Resort>().Property(x => x.Rating).HasPrecision(2, 1);
        b.Entity<Transport>().Property(x => x.Fare).HasPrecision(12, 0);
        b.Entity<Transport>().Property(x => x.DurationHours).HasPrecision(4, 1);
        b.Entity<Package>().Property(x => x.Price).HasPrecision(12, 0);

        // ---- DATE columns (no time part) ----
        b.Entity<Package>().Property(x => x.ValidFrom).HasColumnType("date");
        b.Entity<Package>().Property(x => x.ValidUntil).HasColumnType("date");
        b.Entity<Booking>().Property(x => x.TravelDate).HasColumnType("date");

        // ---- CreatedAt is filled by the database default SYSDATETIME() ----
        b.Entity<TouristSpot>().Property(x => x.CreatedAt).HasDefaultValueSql("SYSDATETIME()");
        b.Entity<Hotel>().Property(x => x.CreatedAt).HasDefaultValueSql("SYSDATETIME()");
        b.Entity<Restaurant>().Property(x => x.CreatedAt).HasDefaultValueSql("SYSDATETIME()");
        b.Entity<Resort>().Property(x => x.CreatedAt).HasDefaultValueSql("SYSDATETIME()");
        b.Entity<Customer>().Property(x => x.CreatedAt).HasDefaultValueSql("SYSDATETIME()");
        b.Entity<Booking>().Property(x => x.CreatedAt).HasDefaultValueSql("SYSDATETIME()");
        b.Entity<Feedback>().Property(x => x.CreatedAt).HasDefaultValueSql("SYSDATETIME()");
        b.Entity<Admin>().Property(x => x.CreatedAt).HasDefaultValueSql("SYSDATETIME()");

        // ---- Relationships: deleting a location must not delete its places ----
        b.Entity<TouristSpot>().HasOne(x => x.Location).WithMany(l => l.TouristSpots)
            .HasForeignKey(x => x.LocationId).OnDelete(DeleteBehavior.Restrict);
        b.Entity<Hotel>().HasOne(x => x.Location).WithMany(l => l.Hotels)
            .HasForeignKey(x => x.LocationId).OnDelete(DeleteBehavior.Restrict);
        b.Entity<Restaurant>().HasOne(x => x.Location).WithMany(l => l.Restaurants)
            .HasForeignKey(x => x.LocationId).OnDelete(DeleteBehavior.Restrict);
        b.Entity<Resort>().HasOne(x => x.Location).WithMany(l => l.Resorts)
            .HasForeignKey(x => x.LocationId).OnDelete(DeleteBehavior.Restrict);
        b.Entity<Package>().HasOne(x => x.Location).WithMany(l => l.Packages)
            .HasForeignKey(x => x.LocationId).OnDelete(DeleteBehavior.Restrict);

        // A route has two links to Locations (from and to)
        b.Entity<Transport>().HasOne(x => x.FromLocation).WithMany()
            .HasForeignKey(x => x.FromLocationId).OnDelete(DeleteBehavior.Restrict);
        b.Entity<Transport>().HasOne(x => x.ToLocation).WithMany()
            .HasForeignKey(x => x.ToLocationId).OnDelete(DeleteBehavior.Restrict);

        // Package <-> TouristSpot many-to-many through PackageSpots (cascade, as in SQL)
        b.Entity<PackageSpot>().HasOne(x => x.Package).WithMany(p => p.PackageSpots)
            .HasForeignKey(x => x.PackageId).OnDelete(DeleteBehavior.Cascade);
        b.Entity<PackageSpot>().HasOne(x => x.Spot).WithMany(s => s.PackageSpots)
            .HasForeignKey(x => x.SpotId).OnDelete(DeleteBehavior.Cascade);

        b.Entity<Booking>().HasOne(x => x.Customer).WithMany(c => c.Bookings)
            .HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.Restrict);
        b.Entity<Feedback>().HasOne(x => x.Customer).WithMany(c => c.Feedbacks)
            .HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.Restrict);
    }
}
