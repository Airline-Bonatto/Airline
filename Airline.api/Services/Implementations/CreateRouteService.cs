using Airline.DTO;
using Airline.Exceptions;
using Airline.Models;
using Airline.Repositories.Interfaces;
using Airline.RequestBodies;
using Airline.Services.Interfaces;

using Route = Airline.Models.Route;

namespace Airline.Services.Implementations;

public class CreateRouteService(
    IRouteRepository routeRepository,
    IAircraftRepository aircraftRepository,
    ICalculateRoutePriceService calculateRoutePriceService
    ) : ICreateRouteService
{
    private readonly IRouteRepository _routeRepository = routeRepository;
    private readonly IAircraftRepository _aircraftRepository = aircraftRepository;
    private readonly ICalculateRoutePriceService _calculateRoutePriceService = calculateRoutePriceService;

    public async Task<int> CreateAsync(RouteInsertRequestBody data)
    {

        Aircraft? aircraft = _aircraftRepository.GetAircraft(data.AircraftId);
        if(aircraft == null)
        {
            throw new EntityNotFoundException("Aircraft not found");
        }

        decimal price = _calculateRoutePriceService.CalculateRoutePrice(data);
        RouteMergeDTO mergeData = new(data, price);

        Route route = new(mergeData)
        {
            Aircraft = aircraft
        };

        return await _routeRepository.InsertAsync(route);
    }
}