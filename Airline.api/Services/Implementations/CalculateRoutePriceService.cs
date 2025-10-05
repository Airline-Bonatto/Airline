using Airline.Models;
using Airline.Repositories.Interfaces;
using Airline.RequestBodies;
using Airline.Services.Interfaces;

namespace Airline.Services.Implementations;

public class CalculateRoutePriceService(IAircraftRepository aircraftRepository) : ICalculateRoutePriceService
{
    private readonly IAircraftRepository _aircraftRepository = aircraftRepository;

    public decimal CalculateRoutePrice(RouteInsertRequestBody routeData)
    {

        Aircraft aircraft = _aircraftRepository.GetAircraft(routeData.AircraftId) ?? throw new ArgumentException("Aircraft not found!");

        decimal price = (decimal)aircraft.AverageFuelConsumption * (decimal)routeData.Distance * 3.5m;
        price /= aircraft.Capacity;

        return price;
    }



}
