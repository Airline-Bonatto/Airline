namespace AirlineAPI.Airline.api.RequestBodies;

public record AircraftUpdateRequestBody(
    int Capacity,
    int Range,
    double AverageFuelConsumption
);