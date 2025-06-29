using Airline.Database;

using AirlineAPI.DTO;

using Microsoft.Data.SqlClient;

namespace AirlineAPI;

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
}
