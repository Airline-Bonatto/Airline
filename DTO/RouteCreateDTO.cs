using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineAPI.DTO
{
    public record RouteCreateDTO(int AircraftId, string From, string To, double Distance);
}