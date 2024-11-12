
using AirlineAPI.DTO;
using AirlineAPI.Exceptions;
using AirlineAPI.Models;

namespace AirlineAPI.Verifications.Route
{
    public class RouteDistanceVerification : ICreateRouteVerification
    {
        

        public void Verify(RouteCreateDTO routeData, Aircraft aircraft)
        {
            if(routeData.Distance > aircraft.Range)
            {
                throw new AircraftRangeException("This aircraft doesn't suport a route with this range");
            }
        }
    }
}