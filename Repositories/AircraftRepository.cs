using Airline.Database;
using AirlineAPI.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AirlineAPI;

public class AircraftRepository : IAircraftRepository
{
    private readonly AirlineContext _context;

    public AircraftRepository(AirlineContext context)
    {
        _context = context;
    }

    public IEnumerable<AircraftListDataView> GetAircrafts()
    {
        return _context.Set<AircraftListDataView>()
            .FromSqlRaw("EXEC Airline.dbo.ListAircrafts")
            .ToList();
    }
    
    public void Insert(AircraftCreateDTO createData)
    {
       _context.Database.ExecuteSqlRaw("EXEC Airline.dbo.InsertAircraft @Model, @Capacity, @Range",
            new SqlParameter("@Model", createData.Model),
            new SqlParameter("@Capacity", createData.Capacity),
            new SqlParameter("@Range", createData.Range)
        );
    }
}
