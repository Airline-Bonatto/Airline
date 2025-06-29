using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AirlineAPI.RequestBodies;

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
    )
    {
        public RouteMergeDTO(RouteInsertRequestBody requestBody, double price, DateTime? finalDate = null)
            : this(
                requestBody.AircraftId,
                requestBody.From,
                requestBody.To,
                requestBody.Distance,
                requestBody.Arrival.ToString("yyyy-MM-ddTHH:mm:ss"),
                requestBody.Departure.ToString("yyyy-MM-ddTHH:mm:ss"),
                price,
                finalDate)
        {
        }
    }
}