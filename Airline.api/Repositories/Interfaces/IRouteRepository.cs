using AirlineAPI.Airline.api.DTO;
using Route = AirlineAPI.Airline.api.Models.Route;

namespace AirlineAPI.Airline.api.Repositories.Interfaces;

public interface IRouteRepository
{
    public Task Insert(RouteMergeDTO createData);
    public Task<List<Route>> List(RouteListFiltersDTO filters);
}
