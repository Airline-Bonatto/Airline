using Airline.Models;

namespace Airline.DTO;

public class FlightDetailDTO(Flight flight)
{
    public int FlightId { get; set; } = flight.FlightId;
    public DateTime Departure { get; set; } = flight.Departure.UtcDateTime;
    public DateTime Arrival { get; set; } = flight.Arrival.UtcDateTime;
    public string AircraftModel { get; set; } = flight.Aircraft.Model;
    public string From { get; set; } = flight.Route.From;
    public string To { get; set; } = flight.Route.To;
}