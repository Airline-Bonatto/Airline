using Airline.DAL;
using AirlineAPI.DTO;
using AirlineAPI.Exceptions;
using AirlineAPI.Models;
using AirlineAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AirlineAPI.Controllers
{
    [ApiController]
    [Route("route")]
    public class RouteController: ControllerBase
    {
        [HttpPost("create")]
        public IResult Create(
            DAL<Aircraft> aircraftDal,
            IRouteCreationService routeCreationService,
            [FromBody] RouteCreateDTO createData)
        {
            try{

                routeCreationService.CreateRoute(aircraftDal, createData);
               
            }catch (EntityNotFoundException)
            {
                return Results.BadRequest(new{Message = "Aircraft not found!"});
            }
            catch(AircraftRangeException e)
            {
                return Results.BadRequest(new{e.Message});
            }

            return Results.Created();
            
        }
    }
}