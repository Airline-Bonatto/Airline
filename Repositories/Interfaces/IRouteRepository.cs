using Airline.DTO;

using AirlineAPI.DTO;

using Route = AirlineAPI.Models.Route;

namespace AirlineAPI;

public interface IRouteRepository
{
    public Task Insert(RouteMergeDTO createData);
    public Task<List<Route>> List(RouteListFiltersDTO filters);
}
