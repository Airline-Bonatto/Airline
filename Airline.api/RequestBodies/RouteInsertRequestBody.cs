namespace Airline.RequestBodies;

public record RouteInsertRequestBody(
    string From,
    string To,
    double Distance
);

