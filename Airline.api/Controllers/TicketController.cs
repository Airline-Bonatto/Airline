
using Airline.DTO;
using Airline.Exceptions;
using Airline.Services.Implementations;

using Microsoft.AspNetCore.Mvc;

namespace Airline.Controllers;

[ApiController]
[Route("ticket")]
public class TicketController(
    TicketPurchaseService ticketPurchaseService
) : ControllerBase
{
    private readonly TicketPurchaseService _ticketPurchaseService = ticketPurchaseService;

    [HttpPost("purchase")]
    public async Task<IActionResult> PurchaseTicket([FromBody] TicketPurchaseRequestDTO request)
    {
        try
        {
            int ticketId = await _ticketPurchaseService.PurchaseTicketAsync(request);
            return Ok(new { TicketId = ticketId });
        }catch(EntityNotFoundException ex)
        {
            return NotFound(new {ex.Message });
        }catch(Exception)
        {
            return StatusCode(500, new { Message = "An error occurred while processing the request." });
        }
    }
}