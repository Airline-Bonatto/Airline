using AirlineAPI.RequestBodies;

namespace AirlineAPI.Services.Interfaces;

public interface ICalculateRoutePriceService
{
    public double CalculateRoutePrice(RouteInsertRequestBody routeData);
}
