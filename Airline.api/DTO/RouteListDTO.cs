
namespace Airline.DTO;

public record RouteListDTO(
    int RouteID,
    string From,
    string To,
    DateTimeOffset Departure,
    DateTimeOffset Arrival,
    decimal Price
);