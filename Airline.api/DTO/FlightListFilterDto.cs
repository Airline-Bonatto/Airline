
using System.ComponentModel.DataAnnotations;

namespace Airline.DTO;

public class FlightListFilterDto
{
    [Required]
    public string From { get; set; } = string.Empty;
    [Required]
    public string To { get; set; } = string.Empty;
    public DateTime? StartDepartureDate { get; set; }
    public DateTime? EndDepartureDate { get; set; }
}