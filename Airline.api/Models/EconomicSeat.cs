
using Airline.DTO;

namespace Airline.Models;

public class EconomicSeat : Seat
{
    public EconomicSeat(SeatCreateDTO data) : base(data)
    {
        SetPrice(data);
    }
}