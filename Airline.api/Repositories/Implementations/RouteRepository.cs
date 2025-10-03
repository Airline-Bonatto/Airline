using Airline.Database;
using Airline.DTO;
using Airline.Repositories.Interfaces;

using Route = Airline.Models.Route;



namespace Airline.Repositories.Implementations;

public class RouteRepository(AirlineContext context) : IRouteRepository
{
    private readonly AirlineContext _context = context;
    public async Task Insert(RouteMergeDTO createData)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Route>> List(RouteListFiltersDTO filters)
    {
        throw new NotImplementedException();
    }
}
