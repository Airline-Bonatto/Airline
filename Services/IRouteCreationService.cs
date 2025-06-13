
using Airline.DAL;
using AirlineAPI.DTO;
using AirlineAPI.Models;

namespace AirlineAPI.Services
{
    public interface IRouteCreationService
    {
        void CreateRoute(DAL<Aircraft> aircraftDal, RouteCreateDTO createData);
    }
}