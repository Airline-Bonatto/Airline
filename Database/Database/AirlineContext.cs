using AirlineAPI;
using AirlineAPI.Models;

using Microsoft.EntityFrameworkCore;

namespace Airline.Database
{
    public class AirlineContext : DbContext
    {

        public DbSet<Aircraft> Aircraft { get; set; }
        public DbSet<AirlineAPI.Models.Route> Route { get; set; }
        public DbSet<AircraftListDataView> AircraftListDataView { get; set; }

        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Airline;Integrated Security=True;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        public AirlineContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(optionsBuilder.IsConfigured)
            {
                return;
            }
            optionsBuilder
                .UseSqlServer(connectionString)
                .UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aircraft>()
                .HasMany(a => a.Routes)
                .WithOne(r => r.Aircraft);

            modelBuilder.Entity<AircraftListDataView>()
                .HasNoKey();
        }

    }
}
