using Airline.RequestBodies;

namespace Airline.DTO;

public record RouteMergeDTO(
    string From,
    string To,
    double Distance
)
{
    public RouteMergeDTO(RouteInsertRequestBody requestBody)
        : this(
            requestBody.From,
            requestBody.To,
            requestBody.Distance)
    {
    }
}