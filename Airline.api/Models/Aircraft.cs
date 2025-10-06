using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Airline.DTO;

namespace Airline.Models;

public class Aircraft
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AircraftID { get; set; }
    public string Model { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public double Range { get; set; }
    public double AverageFuelConsumption { get; set; }

    public Aircraft() { }
    public Aircraft(AircraftCreateDTO data)
    {
        Model = data.Model;
        Capacity = data.Capacity;
        Range = data.Range;
        AverageFuelConsumption = data.AverageFuelConsumption;
    }
}
