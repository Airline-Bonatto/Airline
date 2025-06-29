namespace AirlineAPI.RequestBodies;

public record RouteInsertRequestBody(
    int AircraftId,
    string From,
    string To,
    double Distance,
    DateTime Arrival,
    DateTime Departure
);

