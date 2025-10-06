using Airline.DTO;
using Airline.Exceptions;
using Airline.Models;
using Airline.Repositories.Interfaces;
using Airline.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace Airline.Controllers;


[ApiController]
[Route("flight")]
public class FlightController(
    IFlightCreateService flightCreateService,
    IFlightDetailService flightDetailService
    ) : ControllerBase
{

    private readonly IFlightCreateService _flightCreateService = flightCreateService;
    private readonly IFlightDetailService _flightDetailService = flightDetailService;

    [HttpPost("create")]
    public async Task<IResult> Create([FromBody] FlightCreateDTO data)
    {
        try
        {
            int flightId = await _flightCreateService.Create(data);
            return Results.Created();
        }
        catch(EntityNotFoundException ex)
        {
            return Results.NotFound(new { Message = ex.Message });
        }
    }

    [HttpGet("{flightId}")]
    public async Task<IResult> Detail([FromRoute] int flightId)
    {
        try
        {
            FlightDetailDTO flight = await _flightDetailService.Detail(flightId);
            return Results.Ok(flight);
        }
        catch(EntityNotFoundException ex)
        {
            return Results.NotFound(new { Message = ex.Message });
        }   
    }
}