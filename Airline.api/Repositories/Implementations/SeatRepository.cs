using Airline.Database;
using Airline.Models;
using Airline.Repositories.Interfaces;

namespace Airline.Repositories.Implementations;

public class SeatRepository(AirlineContext context) : ISeatRepository
{
    private readonly AirlineContext _context = context;

    public async Task AddRangeAsync(IEnumerable<Seat> seats)
    {
        _context.Seats.AddRange(seats);
        await _context.SaveChangesAsync();
    }
}