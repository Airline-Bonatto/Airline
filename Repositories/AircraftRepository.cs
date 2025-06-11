using Airline.Database;
using Microsoft.EntityFrameworkCore;

namespace AirlineAPI;

public class AircraftRepository : IAircraftRepository
{
    private readonly AirlineContext _context;

    public AircraftRepository(AirlineContext context)
    {
        _context = context;
    }

    public  IEnumerable<AircraftListDataView> GetAircraftsByCapacity()
    {
        return  _context.Set<AircraftListDataView>()
            .FromSqlRaw("EXEC Airline.dbo.ListAircrafts")
            .ToList();
    }
}
