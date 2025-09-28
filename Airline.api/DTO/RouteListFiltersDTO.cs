namespace AirlineAPI.Airline.api.DTO;

using Microsoft.AspNetCore.Mvc;

public record RouteListFiltersDTO
{
    [FromQuery(Name = "aircraftId")]
    public int? AircraftId { get; init; } = null;

    [FromQuery(Name = "from")]
    public string? From { get; init; } = null;

    [FromQuery(Name = "to")]
    public string? To { get; init; } = null;
}
