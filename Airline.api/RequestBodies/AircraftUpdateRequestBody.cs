namespace Airline.RequestBodies;

public record AircraftUpdateRequestBody(
    int? Capacity,
    int? Range,
    double? AverageFuelConsumption
);