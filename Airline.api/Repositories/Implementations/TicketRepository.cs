using Airline.Database;
using Airline.Models;

namespace Airline.Repositories.Implementations;

public class TicketRepository(AirlineContext context) 
{
    public readonly AirlineContext _context = context;

    public async Task<int> AddTicketAsync(Ticket ticket)
    {
        _context.Tickets.Add(ticket);
        await  _context.SaveChangesAsync();
        return ticket.TicketId;
    }
}