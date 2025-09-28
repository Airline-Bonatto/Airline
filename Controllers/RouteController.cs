
using Airline.DTO;

using AirlineAPI.DTO;
using AirlineAPI.RequestBodies;
using AirlineAPI.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

using Route = AirlineAPI.Models.Route;

namespace AirlineAPI.Controllers;

[ApiController]
[Route("route")]
public class RouteController(IRouteRepository routeRepository) : ControllerBase
{

    private readonly IRouteRepository _routeRepository = routeRepository;
    [HttpPost("create")]
    public async Task<IResult> Create(
        [FromBody] RouteInsertRequestBody createData,
        ICalculateRoutePriceService calculateRoutePriceService)
    {
        double price = calculateRoutePriceService.CalculateRoutePrice(createData);
        RouteMergeDTO mergeData = new(createData, price);
        await _routeRepository.Insert(mergeData);

        return Results.Created();
    }


    [HttpGet("list")]
    public async Task<IResult> List([FromQuery] RouteListFiltersDTO filters)
    {
        List<Route> routes = await _routeRepository.List(filters);
        return Results.Ok(routes);
    }
}


