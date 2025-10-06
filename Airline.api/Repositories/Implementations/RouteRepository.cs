using Airline.Database;
using Airline.DTO;
using Airline.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;

using Route = Airline.Models.Route;



namespace Airline.Repositories.Implementations;

public class RouteRepository(AirlineContext context) : IRouteRepository
{
    private readonly AirlineContext _context = context;
    public async Task<int> InsertAsync(Route route)
    {
        await _context.Routes.AddAsync(route);
        await _context.SaveChangesAsync();
        return route.RouteID;
    }

    public async Task<IEnumerable<RouteListDTO>> ListAsync(RouteListFiltersDTO filters)
    {
        var query = _context.Routes.AsQueryable();

        if(!string.IsNullOrEmpty(filters.From))
        {
            query = query.Where(r => r.From == filters.From);
        }

        if(!string.IsNullOrEmpty(filters.To))
        {
            query = query.Where(r => r.To == filters.To);
        }

        List<RouteListDTO> routes = await query
            .Select(r => new RouteListDTO(
                r.RouteID,
                r.From,
                r.To
            )).ToListAsync();

        return routes;
    }
}
