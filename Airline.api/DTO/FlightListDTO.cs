using Airline.Models;

namespace Airline.DTO;

public class FlightListDTO(Flight flight)
{
    public int FlightId { get; set; } = flight.FlightId;
    public DateTimeOffset Departure { get; set; } = flight.Departure;
    public DateTimeOffset Arrival { get; set; } = flight.Arrival;
}