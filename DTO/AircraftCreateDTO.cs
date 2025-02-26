
using System.ComponentModel.DataAnnotations;

namespace AirlineAPI.DTO
{
    public record AircraftCreateDTO(
        [Required]
        string Model, 

        [Required]
        int Capacity, 
        
        [Required]
        double Range
    );
}