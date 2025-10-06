using Airline.DTO;

namespace Airline.Services.Interfaces;

public interface IFlightCreateService
{
    public Task<int> Create(FlightCreateDTO data);
}