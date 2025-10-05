using Airline.DTO;

using Route = Airline.Models.Route;

namespace Airline.Repositories.Interfaces;

public interface IRouteRepository
{
    public Task<int> InsertAsync(Route route);
    public Task<IEnumerable<RouteListDTO>> ListAsync(RouteListFiltersDTO filters);
}
