using Airline.Models;

using Microsoft.EntityFrameworkCore;

using Route = Airline.Models.Route;

namespace Airline.Database;

public class AirlineContext(DbContextOptions<AirlineContext> options) : DbContext(options)
{

    public DbSet<Aircraft> Aircrafts { get; set; }
    public DbSet<Route> Routes { get; set; }
    public DbSet<Flight> Flights { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // modelBuilder.Entity<Aircraft>()
        //     .HasMany(a => a.Routes)
        //     .WithOne(r => r.Aircraft);
    }

}
