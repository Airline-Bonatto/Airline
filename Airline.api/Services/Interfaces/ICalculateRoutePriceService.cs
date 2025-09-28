using AirlineAPI.Airline.api.RequestBodies;

namespace AirlineAPI.Airline.api.Services.Interfaces;

public interface ICalculateRoutePriceService
{
    public double CalculateRoutePrice(RouteInsertRequestBody routeData);
}
