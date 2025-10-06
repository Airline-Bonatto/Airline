
using Airline.DTO;
using Airline.Exceptions;
using Airline.Models;
using Airline.Repositories.Interfaces;
using Airline.Services.Interfaces;

namespace Airline.Services.Implementations;

public class FlightDetailService(
    IFlightRepository flightRepository
) : IFlightDetailService
{
    private readonly IFlightRepository _flightRepository = flightRepository;
    public async Task<FlightDetailDTO> Detail(int flightId)
    {

        Flight? flight = await _flightRepository.GetByIdAsync(flightId);
        if (flight == null)
        {
            throw new EntityNotFoundException("Flight not found");
        }

        return new FlightDetailDTO(flight);
    }
}