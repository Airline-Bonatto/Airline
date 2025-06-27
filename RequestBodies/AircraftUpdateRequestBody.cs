namespace AirlineAPI;

public record AircraftUpdateRequestBody(
    int Capacity,
    int Range,
    double AvaregeFuelConsumption
);