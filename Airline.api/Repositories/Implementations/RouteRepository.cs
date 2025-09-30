using Airline.Database;
using Airline.DTO;
using Airline.Helpers;
using Airline.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

using Route = Airline.Models.Route;



namespace Airline.Repositories.Implementations;

public class RouteRepository(AirlineContext context) : IRouteRepository
{
    private readonly AirlineContext _context = context;
    public async Task Insert(RouteMergeDTO createData)
    {
        await SqlHelper.ExecStoredProcedureAsync(
            _context,
            "Airline.dbo.MergeRoute",
            new SqlParameter("@aircraftId", createData.AircraftId),
            new SqlParameter("@from", createData.From),
            new SqlParameter("@to", createData.To),
            new SqlParameter("@distance", createData.Distance),
            new SqlParameter("@arrival", createData.Arrival),
            new SqlParameter("@departure", createData.Departure),
            new SqlParameter("@price", createData.Price)
        );
    }

    public async Task<List<Route>> List(RouteListFiltersDTO filters)
    {
        IQueryable<Route> query = _context.Route.AsQueryable();

        if(filters.AircraftId.HasValue)
        {
            query = query.Where(r => r.Aircraft.AircraftID == filters.AircraftId.Value);
        }

        if(!string.IsNullOrEmpty(filters.From))
        {
            query = query.Where(r => r.From == filters.From);
        }

        if(!string.IsNullOrEmpty(filters.To))
        {
            query = query.Where(r => r.To == filters.To);
        }

        return await query.ToListAsync();
    }
}
