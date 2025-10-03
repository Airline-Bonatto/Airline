using Airline.Models;

using Microsoft.EntityFrameworkCore;

using Route = Airline.Models.Route;

namespace Airline.Database;

public class AirlineContext(DbContextOptions<AirlineContext> options) : DbContext(options)
{

    public DbSet<Aircraft> Aircraft { get; set; }
    public DbSet<Route> Route { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aircraft>()
            .HasMany(a => a.Routes)
            .WithOne(r => r.Aircraft);
    }

}
