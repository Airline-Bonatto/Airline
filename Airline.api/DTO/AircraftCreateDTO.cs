
using System.ComponentModel.DataAnnotations;

namespace AirlineAPI.Airline.api.DTO;

public record AircraftCreateDTO(
    [Required]
    string Model,

    [Required]
    int Capacity,

    [Required]
    double Range,

    [Required]
    double AverageFuelConsumption
);