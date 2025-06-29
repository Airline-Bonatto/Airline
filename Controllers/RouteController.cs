
using AirlineAPI.DTO;
using AirlineAPI.Models;

using Microsoft.AspNetCore.Mvc;

namespace AirlineAPI.Controllers
{
    [ApiController]
    [Route("route")]
    public class RouteController : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IResult> Create(
            [FromBody] RouteMergeDTO createData,
            [FromServices] IRouteRepository routeRepository)
        {
            await routeRepository.Insert(createData);
            return Results.Created();
        }
    }
}