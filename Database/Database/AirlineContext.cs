


using AirlineAPIV2.Models;
using Microsoft.EntityFrameworkCore;

namespace Airline.Database
{
    public class AirlineContext : DbContext
    {

        public DbSet<Aircraft> Aircraft {  get; set; }
        public DbSet<AirlineAPIV2.Models.Route> Route { get; set; }

        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Airline;Integrated Security=True;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(connectionString)
                .UseLazyLoadingProxies();
        }

    }
}
