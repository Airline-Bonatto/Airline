using Airline.Models;

namespace Airline.Repositories.Interfaces;

public interface IFlightRepository
{
    public Task<int> Create(Flight flight);
    public Task<Flight?> GetByIdAsync(int id);
}