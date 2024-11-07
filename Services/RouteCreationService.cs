
using Airline.DAL;
using AirlineAPI.DTO;
using AirlineAPI.Exceptions;
using AirlineAPI.Models;

namespace AirlineAPI.Services
{
    public class RouteCreationService() : IRouteCreationService
    {
        public void CreateRoute(DAL<Aircraft> aircraftDal, RouteCreateDTO createData)
        {
             try{
                var aircraft = aircraftDal.GetById(createData.AircraftId);
                var route = new Models.Route
                {
                    Aircraft = aircraft, 
                    From = createData.From, 
                    To = createData.To, 
                    Distance = createData.Distance
                };

                aircraft.AddRoute(route);
                aircraftDal.Update(aircraft);
            }catch (EntityNotFoundException)
            {
                throw;
            }
        }
    }
}