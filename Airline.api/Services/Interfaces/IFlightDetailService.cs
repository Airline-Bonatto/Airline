using Airline.DTO;

namespace Airline.Services.Interfaces;

public interface IFlightDetailService
{
    public Task<FlightDetailDTO> Detail(int flightId);
}