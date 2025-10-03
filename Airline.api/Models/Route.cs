
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airline.Models;

public class Route
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RouteID { get; set; }
    public required virtual Aircraft Aircraft { get; set; }
    public required string From { get; set; }
    public required string To { get; set; }
    public DateTime Departure { get; set; }
    public DateTime Arrival { get; set; }
    public double Distance { get; set; }
}
