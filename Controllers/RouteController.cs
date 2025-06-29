
using AirlineAPI.DTO;
using AirlineAPI.Models;
using AirlineAPI.RequestBodies;
using AirlineAPI.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace AirlineAPI.Controllers
{
    [ApiController]
    [Route("route")]
    public class RouteController : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IResult> Create(
            [FromBody] RouteInsertRequestBody createData,
            IRouteRepository routeRepository,
            ICalculateRoutePriceService calculateRoutePriceService)
        {
            double price = calculateRoutePriceService.CalculateRoutePrice(createData);
            RouteMergeDTO mergeData = new(createData, price);
            await routeRepository.Insert(mergeData);

            return Results.Created();
        }
    }
}