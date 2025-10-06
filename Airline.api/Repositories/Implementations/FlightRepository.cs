using Airline.Database;
using Airline.Models;
using Airline.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Airline.Repositories.Implementations;

public class FlightRepository(AirlineContext context) : IFlightRepository
{
    private readonly AirlineContext _context = context;

    public async Task<int> Create(Flight flight)
    {
        _context.Flights.Add(flight);
        await _context.SaveChangesAsync();
        return flight.FlightId;
    }

    public async Task<Flight?> GetByIdAsync(int id)
    {
        return await _context.Flights
            .Include(f => f.Aircraft)
            .Include(f => f.Route)
            .FirstOrDefaultAsync(f => f.FlightId == id);
    }
}