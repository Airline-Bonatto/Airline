using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Airline.DTO;
using Airline.Enuns;

namespace Airline.Models;

public class Seat
{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SeatId { get; set; }
    public int SeatNumber { get; set; }
    public string Row { get; set; } = null!;
    public bool IsAvailable { get; set; }
    public decimal Price { get; set; }
    public SeatClassEnum SeatClass { get; set; }
    public int FlightId { get; set; }
    public virtual Flight Flight { get; set; } = null!;


    public Seat() { }
    protected Seat(SeatCreateDTO data)
    {
        SeatNumber = data.SeatNumber;
        Row = data.Row;
        IsAvailable = data.IsAvailable;
        SeatClass = data.SeatClass;
    }

    public static Seat Create(SeatCreateDTO data)
    {
        return data.SeatClass switch
        {
            SeatClassEnum.FirstClass => new FirstClassSeat(data),
            SeatClassEnum.Executive => new ExecutiveSeat(data),
            SeatClassEnum.Economic => new EconomicSeat(data),
            _ => throw new ArgumentOutOfRangeException(nameof(data.SeatClass), "Invalid seat class")
        };
    }
    protected virtual void SetPrice(SeatCreateDTO data)
    {
        Price = (decimal)data.AircraftAverageFuelConsumption * (decimal)data.Distance * 3.5m;
        Price /= data.AircraftCapacity;
    }

}