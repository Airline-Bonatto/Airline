using Airline.DAL;
using AirlineAPI.DTO;
using AirlineAPI.Exceptions;
using AirlineAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace AirlineAPI.Controllers
{
    [ApiController]
    [Route("route")]
    public class RouteController: ControllerBase
    {
        [HttpPost("create")]
        public IResult Create(
            [FromServices] DAL<Aircraft> aircraftDal,
            [FromBody] RouteCreateDTO createData)
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
                return Results.BadRequest("Aircraft not found!");
            }

            return Results.Created();
            
        }
    }
}