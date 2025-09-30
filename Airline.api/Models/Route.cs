
namespace Airline.Models;

public class Route
{
    public int RouteID { get; set; }
    public required virtual Aircraft Aircraft { get; set; }
    public required string From { get; set; }
    public required string To { get; set; }
    public DateTime Departure { get; set; }
    public DateTime Arrival { get; set; }
    public double Distance { get; set; }
}
