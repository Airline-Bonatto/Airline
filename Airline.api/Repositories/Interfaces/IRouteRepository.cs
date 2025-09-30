using Airline.DTO;
using Route = Airline.Models.Route;

namespace Airline.Repositories.Interfaces;

public interface IRouteRepository
{
    public Task Insert(RouteMergeDTO createData);
    public Task<List<Route>> List(RouteListFiltersDTO filters);
}
