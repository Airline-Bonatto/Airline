using AirlineAPI.Airline.api.Dataviews;
using AirlineAPI.Airline.api.Repositories.Interfaces;
using AirlineAPI.Airline.api.RequestBodies;
using AirlineAPI.Airline.api.Services.Interfaces;

namespace AirlineAPI.Airline.api.Services.Implementations;

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
