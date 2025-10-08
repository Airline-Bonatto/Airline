
using Airline.DTO;

namespace Airline.Services.Interfaces;

public interface ISeatCreateService
{
    public Task CreateAsync(SeatCreateRequestDTO data);
}