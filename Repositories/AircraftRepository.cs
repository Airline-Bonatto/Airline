using Airline.Database;
using AirlineAPI.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AirlineAPI;

public class AircraftRepository : IAircraftRepository
{
    private readonly AirlineContext _context;

    public AircraftRepository(AirlineContext context)
    {
        _context = context;
    }

    public IEnumerable<AircraftListDataView> GetAircrafts()
    {

        return SqlHelper.ExecStoredProcedureWithResult<AircraftListDataView>(
            _context,
            "Airline.dbo.ListAircrafts"
        );
    }

    public void Insert(AircraftCreateDTO createData)
    {

        SqlHelper.ExecStoredProcedure(
            _context,
            "Airline.dbo.InsertAircraft",
            new SqlParameter("@Model", createData.Model),
            new SqlParameter("@Capacity", createData.Capacity),
            new SqlParameter("@Range", createData.Range)
        );

    }
}
