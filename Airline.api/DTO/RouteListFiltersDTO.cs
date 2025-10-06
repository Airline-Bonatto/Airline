namespace Airline.DTO;

using Microsoft.AspNetCore.Mvc;

public record RouteListFiltersDTO
{
    [FromQuery(Name = "from")]
    public string? From { get; init; } = null;

    [FromQuery(Name = "to")]
    public string? To { get; init; } = null;
}
