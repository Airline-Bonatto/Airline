
using Airline.RequestBodies;

namespace Airline.Services.Interfaces;

public interface ICalculateRoutePriceService
{
    public double CalculateRoutePrice(RouteInsertRequestBody routeData);
}
