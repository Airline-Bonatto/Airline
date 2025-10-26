
using System.Text.Json.Serialization;

namespace Airline.DTO;

public class TicketPurchaseRequestDTO
{
    [JsonPropertyName("seatId")]
    public int SeatId { get; set; }

    [JsonPropertyName("userId")]
    public int AirlineUserId { get; set; }
}