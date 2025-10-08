using System.Text.Json.Serialization;

namespace Airline.DTO;

public class SeatCreateRequestDTO
{
    [JsonPropertyName("flight_id")]
    public int FlightId { get; set; }
    [JsonPropertyName("quantityEconomic")]
    public int QuantityEconomic { get; set; }
    [JsonPropertyName("quantityExecutive")]
    public int QuantityExecutive { get; set; }
    [JsonPropertyName("quantityFirstClass")]
    public int QuantityFirstClass { get; set; }
}