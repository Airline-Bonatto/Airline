using AirlineAPI;
using AirlineAPI.Dataviews;
using AirlineAPI.Models;

using Microsoft.EntityFrameworkCore;

namespace Airline.Database
{
    public class AirlineContext(DbContextOptions options) : DbContext(options)
    {

        public DbSet<Aircraft> Aircraft { get; set; }
        public DbSet<AirlineAPI.Models.Route> Route { get; set; }
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
}
