using Airline.RequestBodies;

namespace Airline.DTO;

public record RouteMergeDTO(
    int AircraftId,
    string From,
    string To,
    double Distance,
    string Arrival,
    string Departure,
    decimal Price
)
{
    public RouteMergeDTO(RouteInsertRequestBody requestBody, decimal price)
        : this(
            requestBody.AircraftId,
            requestBody.From,
            requestBody.To,
            requestBody.Distance,
            requestBody.Arrival.ToString("yyyy-MM-ddTHH:mm:ss"),
            requestBody.Departure.ToString("yyyy-MM-ddTHH:mm:ss"),
            price)
    {
    }
}