
using Airline.Database;
using Airline.DTO;
using Airline.Models;
using Airline.Repositories.Interfaces;

namespace Airline.Repositories.Implementations;

public class AircraftRepository(AirlineContext context) : IAircraftRepository
{
    private readonly AirlineContext _context = context;

    public Aircraft? GetAircraft(int aircraftId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Aircraft> ListAircrafts()
    {

        throw new NotImplementedException();
    }

    public void Insert(AircraftCreateDTO createData)
    {
        throw new NotImplementedException();

    }

    public void Update(AircraftUpdateDTO updateData)
    {
        throw new NotImplementedException();
    }
}
