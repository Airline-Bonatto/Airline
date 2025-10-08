using Airline.DTO;

namespace Airline.Models;

public class ExecutiveSeat : Seat
{
    public ExecutiveSeat(SeatCreateDTO data) : base(data)
    {
        SetPrice(data);
    }

    protected override void SetPrice(SeatCreateDTO data)
    {
        base.SetPrice(data);
        Price *= 2;
    }
}