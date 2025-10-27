
namespace Airline.Exceptions;

public class TicketPurchaseException : Exception
{
    public TicketPurchaseException() : base("Error trying to purchase a ticket.")
    {
    }
    public TicketPurchaseException(string message)
        : base(message)
    {
    }
}