
using Airline.Database;
using Airline.Dataviews;
using Airline.DTO;
using Airline.Helpers;
using Airline.Repositories.Interfaces;

using Microsoft.Data.SqlClient;

namespace Airline.Repositories.Implementations;

public class AircraftRepository(AirlineContext context) : IAircraftRepository
{
    private readonly AirlineContext _context = context;

    public AircraftDetailView? GetAircraft(int aircraftId)
    {
        return SqlHelper.ExecStoredProcedureWithResult<AircraftDetailView>(
            _context,
            "Airline.dbo.GetAircraft",
            new SqlParameter("@aircraftId", aircraftId)
        ).FirstOrDefault();
    }

    public IEnumerable<AircraftListDataView> ListAircrafts()
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
        "Airline.dbo.MergeAircraft",
        new SqlParameter("@model", createData.Model),
        new SqlParameter("@capacity", createData.Capacity),
        new SqlParameter("@range", createData.Range),
        new SqlParameter("@averageFuelConsumption", createData.AverageFuelConsumption)
    );

    }

    public void Update(AircraftUpdateDTO updateData)
    {
        SqlHelper.ExecStoredProcedure(
            _context,
            "Airline.dbo.MergeAircraft",
            new SqlParameter("@aircraftId", updateData.AircraftId),
            new SqlParameter("@capacity", updateData.Capacity),
            new SqlParameter("@range", updateData.Range),
            new SqlParameter("@avaregeFuelConsumption", updateData.AverageFuelConsumption),
            new SqlParameter("@finalDate", updateData.FinalDate)
        );
    }
}
