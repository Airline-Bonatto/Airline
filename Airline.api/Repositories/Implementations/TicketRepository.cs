using Airline.Database;
using Airline.Models;

using Microsoft.EntityFrameworkCore;

namespace Airline.Repositories.Implementations;

public class TicketRepository(AirlineContext context)
{
    public readonly AirlineContext _context = context;

    public async Task<int> AddTicketAsync(Ticket ticket)
    {
        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();
        return ticket.TicketId;
    }

    public async Task<Ticket?> GetByOwnerDocumentAndFlightAsync(string ownerDocument, int flightId)
    {
        return await _context.Tickets
            .Include(t => t.Seat)
            .Where(t => t.OwnerDocument == ownerDocument)
            .Where(t => t.Seat.FlightId == flightId)
            .FirstOrDefaultAsync();
    }
}