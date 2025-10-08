using Airline.DTO;
using Airline.Exceptions;
using Airline.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace Airline.Controllers;

[ApiController]
[Route("seat")]
public class SeatController(
    ISeatCreateService seatCreateService
) : ControllerBase
{
    private readonly ISeatCreateService _seatCreateService = seatCreateService;

    [HttpPost("create")]
    public async Task<IResult> Create([FromBody] SeatCreateRequestDTO createData)
    {
        try
        {
            await _seatCreateService.CreateAsync(createData);
            return Results.Created();
        }
        catch(EntityNotFoundException e)
        {
            return Results.NotFound(new { Message = e.Message });
        }
    }
}