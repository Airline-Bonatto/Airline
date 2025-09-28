
using AirlineAPI.Airline.api.DTO;
using AirlineAPI.Airline.api.Repositories.Interfaces;
using AirlineAPI.Airline.api.RequestBodies;
using AirlineAPI.Airline.api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Route = AirlineAPI.Airline.api.Models.Route;

namespace AirlineAPI.Airline.api.Controllers;

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


