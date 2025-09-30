using Airline.Dataviews;
using Airline.Repositories.Interfaces;
using Airline.RequestBodies;
using Airline.Services.Interfaces;

namespace Airline.Services.Implementations;

public class CalculateRoutePriceService(IAircraftRepository aircraftRepository) : ICalculateRoutePriceService
{
    private readonly IAircraftRepository _aircraftRepository = aircraftRepository;

    public double CalculateRoutePrice(RouteInsertRequestBody routeData)
    {

        AircraftDetailView aircraft = _aircraftRepository.GetAircraft(routeData.AircraftId) ?? throw new ArgumentException("Aircraft not found!");

        double price = aircraft.AverageFuelConsumption * routeData.Distance * 3.5;
        price /= aircraft.Capacity;

        return price;
    }



}
