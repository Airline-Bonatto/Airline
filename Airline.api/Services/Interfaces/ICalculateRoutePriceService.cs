
using Airline.RequestBodies;

namespace Airline.Services.Interfaces;

public interface ICalculateRoutePriceService
{
    public decimal CalculateRoutePrice(RouteInsertRequestBody routeData);
}
