using Airline.DTO;
using Airline.Repositories.Interfaces;

namespace Airline.Services.Implementations;

public class SeatListService(ISeatRepository seatRepository)
{
    private readonly ISeatRepository _seatRepository = seatRepository;

    public async Task<List<SeatListDTO>> ListAsync(SeatListFilterDTO filters)
    {
        var seats = await _seatRepository.ListAsync(filters);
        return seats.Select(s => new SeatListDTO(s)).ToList();
    }
}