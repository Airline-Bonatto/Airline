using Airline.DTO;
using Airline.Repositories.Interfaces;
using Airline.RequestBodies;
using Airline.Services.Interfaces;

using Route = Airline.Models.Route;

namespace Airline.Services.Implementations;

public class CreateRouteService(
    IRouteRepository routeRepository
    ) : ICreateRouteService
{
    private readonly IRouteRepository _routeRepository = routeRepository;

    public async Task<int> CreateAsync(RouteInsertRequestBody data)
    {
        RouteMergeDTO mergeData = new(data);
        Route route = new(mergeData);

        return await _routeRepository.InsertAsync(route);
    }
}