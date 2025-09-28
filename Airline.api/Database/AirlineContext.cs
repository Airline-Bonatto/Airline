using AirlineAPI.Airline.api.Dataviews;
using AirlineAPI.Airline.api.Models;

using Microsoft.EntityFrameworkCore;
using Route = AirlineAPI.Airline.api.Models.Route;

namespace AirlineAPI.Airline.api.Database;

public class AirlineContext(DbContextOptions<AirlineContext> options) : DbContext(options)
{

    public DbSet<Aircraft> Aircraft { get; set; }
    public DbSet<Route> Route { get; set; }
    public DbSet<AircraftListDataView> AircraftListDataView { get; set; }
    public DbSet<AircraftDetailView> AircraftDataView { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aircraft>()
            .HasMany(a => a.Routes)
            .WithOne(r => r.Aircraft);

        modelBuilder.Entity<AircraftListDataView>()
            .HasNoKey();
        modelBuilder.Entity<AircraftDetailView>()
            .HasNoKey();
    }

}
