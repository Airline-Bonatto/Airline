using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airline.Models;

public class Flight
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int FlightId { get; set; }
    public virtual Route Route { get; set; } = null!;
    public virtual Aircraft Aircraft { get; set; } = null!;
    public DateTimeOffset Departure { get; set; }
    public DateTimeOffset Arrival { get; set; }
}