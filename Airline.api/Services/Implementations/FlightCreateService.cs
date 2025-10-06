using Airline.DTO;
using Airline.Exceptions;
using Airline.Models;
using Airline.Repositories.Interfaces;
using Airline.Services.Interfaces;

using Route = Airline.Models.Route;

namespace Airline.Services.Implementations;

public class FlightCreateService(
    IFlightRepository _flightRepository,
    IAircraftRepository _aircraftRepository,
    IRouteRepository _routeRepository
) : IFlightCreateService
{

    private readonly IFlightRepository _flightRepository = _flightRepository;
    private readonly IAircraftRepository _aircraftRepository = _aircraftRepository;
    private readonly IRouteRepository _routeRepository = _routeRepository;
    public async Task<int> Create(FlightCreateDTO data)
    {
        Aircraft? aircraft = _aircraftRepository.GetAircraft(data.AircraftId);
        if(aircraft == null)
            throw new EntityNotFoundException("Aircraft not found");

        Route? route = await _routeRepository.GetByIdAsync(data.RouteId);
        if(route == null)
            throw new EntityNotFoundException("Route not found");

        Flight flight = new(data)
        {
            Aircraft = aircraft,
            Route = route
        };

        return await _flightRepository.Create(flight);
    }
}