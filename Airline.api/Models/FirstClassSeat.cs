
using Airline.DTO;

namespace Airline.Models;

public class FirstClassSeat : Seat
{
    public FirstClassSeat(SeatCreateDTO data) : base(data)
    {
        SetPrice(data);
    }


    protected override void SetPrice(SeatCreateDTO data)
    {
        base.SetPrice(data);
        Price += 1000;
        Price *= 3;
    }
}