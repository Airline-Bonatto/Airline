using System.Data;

using Airline.Database;

using AirlineAPI.Dataviews;
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
        new SqlParameter("@avaregeFuelConsumption", createData.AvaregeFuelConsumption)
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
            new SqlParameter("@avaregeFuelConsumption", updateData.AvaregeFuelConsumption),
            new SqlParameter("@finalDate", updateData.FinalDate)
        );
    }
}
