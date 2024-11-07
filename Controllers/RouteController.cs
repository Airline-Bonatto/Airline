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
            [FromServices] DAL<Aircraft> aircraftDal,
            [FromServices] IRouteCreationService routeCreationService,
            [FromBody] RouteCreateDTO createData)
        {
            try{

                routeCreationService.CreateRoute(aircraftDal, createData);
               
            }catch (EntityNotFoundException)
            {
                return Results.BadRequest("Aircraft not found!");
            }

            return Results.Created();
            
        }
    }
}