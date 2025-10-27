
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Airline.DTO;

namespace Airline.Models;

public class Ticket
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TicketId { get; set; }
    public int SeatId { get; set; }
    public int AirlineUserId { get; set; }
    public virtual Seat Seat { get; set; } = null!;
    public virtual AirlineUser AirlineUser { get; set; } = null!;
    public string OwnerDocument { get; set; } = null!;

    public Ticket() { }

    public Ticket(TicketPurchaseRequestDTO data)
    {
        SeatId = data.SeatId;
        AirlineUserId = data.AirlineUserId;
        OwnerDocument = data.OwnerDocument;
    }
}