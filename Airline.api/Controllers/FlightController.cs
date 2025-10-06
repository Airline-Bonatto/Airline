using Airline.DTO;
using Airline.Exceptions;
using Airline.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace Airline.Controllers;


[ApiController]
[Route("flight")]
public class FlightController(IFlightCreateService flightCreateService) : ControllerBase
{

    private readonly IFlightCreateService _flightCreateService = flightCreateService;

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
}