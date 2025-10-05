
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Airline.DTO;

namespace Airline.Models;

public class Route
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RouteID { get; set; }
    public virtual Aircraft Aircraft { get; set; } = null!;
    public string From { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public DateTimeOffset Departure { get; set; }
    public DateTimeOffset Arrival { get; set; }
    public double Distance { get; set; }

    public decimal Price { get; set; }

    public Route() { }

    public Route(RouteMergeDTO data)
    {
        From = data.From;
        To = data.To;
        Departure = DateTimeOffset.Parse(data.Departure).ToUniversalTime();
        Arrival = DateTimeOffset.Parse(data.Arrival).ToUniversalTime();
        Distance = data.Distance;
        Price = data.Price;
    }

}
