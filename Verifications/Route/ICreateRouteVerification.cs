
using AirlineAPI.DTO;
using AirlineAPI.Models;

namespace AirlineAPI.Verifications.Route
{
    public interface ICreateRouteVerification
    {
        void Verify(RouteCreateDTO createData, Aircraft aircraft);
    }
}