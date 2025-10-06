
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Airline.DTO;

namespace Airline.Models;

public class Route
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RouteID { get; set; }
    public string From { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public double Distance { get; set; }
    public Route() { }

    public Route(RouteMergeDTO data)
    {
        From = data.From;
        To = data.To;
        Distance = data.Distance;
    }

}
