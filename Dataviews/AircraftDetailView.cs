using AirlineAPI.Models;

namespace AirlineAPI.Dataviews
{
    public class AircraftDetailView(Aircraft aircraft)
    {
        public int AircraftID { get; } = aircraft.AircraftID;
        public string? Model { get; } = aircraft.Model;
        public int Capacity { get; } = aircraft.Capacity;
        public double Range { get; } = aircraft.Range;
    }
}