using Airline.DTO;
using Airline.Models;

namespace Airline.Repositories.Interfaces;

public interface ISeatRepository
{
    public Task AddRangeAsync(IEnumerable<Seat> seats);
    public Task<IEnumerable<Seat>> ListAsync(SeatListFilterDTO filter);
}