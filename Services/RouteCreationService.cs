
using Airline.DAL;

using AirlineAPI.DTO;
using AirlineAPI.Models;
using AirlineAPI.Verifications.Route;

namespace AirlineAPI.Services
{
    public class RouteCreationService(IEnumerable<ICreateRouteVerification> _verifications) : IRouteCreationService
    {
        public void CreateRoute(DAL<Aircraft> aircraftDal, RouteCreateDTO createData)
        {
            try
            {
                var aircraft = aircraftDal.GetById(createData.AircraftId);

                foreach(var verification in _verifications)
                {
                    verification.Verify(createData, aircraft);
                }

                var route = new Models.Route
                {
                    Aircraft = aircraft,
                    From = createData.From,
                    To = createData.To,
                    Distance = createData.Distance
                };

                aircraft.AddRoute(route);
                aircraftDal.Update(aircraft);
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}