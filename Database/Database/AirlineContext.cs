using AirlineAPIV2.Models;
using Microsoft.EntityFrameworkCore;

namespace Airline.Database
{
    public class AirlineContext : DbContext
    {

        public DbSet<Aircraft> Aircraft {  get; set; }
        public DbSet<AirlineAPIV2.Models.Route> Route { get; set; }

        // private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Airline;Integrated Security=True;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        private string connectionString = "Server=tcp:airline-server.database.windows.net,1433;Initial Catalog=Airline;Persist Security Info=False;User ID=gjusto;Password=DefaultPassword@2024;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=1000;";


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(connectionString)
                .UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aircraft>()
                .HasMany(a=>a.Routes)
                .WithOne(r=>r.Aircraft);
        }

    }
}
