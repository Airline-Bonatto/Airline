
namespace Airline.Dataviews;

public class AircraftDetailView
{
    public int AircraftID { get; set; }
    public string? Model { get; set; }
    public int Capacity { get; set; }
    public double Range { get; set; }
    public double AverageFuelConsumption { get; set; }
}