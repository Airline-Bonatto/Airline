namespace Airline.DTO;

public record FlightCreateDTO(
    int RouteId,
    int AircraftId,
    DateTimeOffset Departure,
    DateTimeOffset Arrival
);