using Airline.Enuns;
using Airline.Models;

namespace Airline.DTO;

public class SeatListDTO
{
    public int SeatId { get; set; }
    public int FlightId { get; set; }
    public int SeatNumber { get; set; }
    public string Row { get; set; } = null!;
    public bool IsAvailable { get; set; }
    public decimal Price { get; set; }
    public SeatClassEnum SeatClass { get; set; }

    public SeatListDTO(Seat seat)
    {
        SeatId = seat.SeatId;
        FlightId = seat.FlightId;
        SeatNumber = seat.SeatNumber;
        Row = seat.Row;
        IsAvailable = seat.IsAvailable;
        Price = seat.Price;
        SeatClass = seat.SeatClass;
    }
}