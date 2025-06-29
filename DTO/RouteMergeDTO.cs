using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineAPI.DTO
{
    public record RouteMergeDTO(
        int AircraftId,
        string From,
        string To,
        double Distance,
        string Arrival,
        string Departure,
        double Price,
        DateTime? FinalDate
    );
}