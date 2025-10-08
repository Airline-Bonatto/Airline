using Airline.Enuns;

namespace Airline.DTO;

public class SeatCreateDTO
{
    public int SeatNumber { get; set; }
    public string Row { get; set; } = null!;
    public bool IsAvailable { get; set; }
    public SeatClassEnum SeatClass { get; set; }
    public int FlightId { get; set; }

    public double AircraftAverageFuelConsumption { get; set; }
    public int AircraftCapacity { get; set; }
    public double Distance { get; set; }
}