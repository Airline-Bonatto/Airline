
using AirlineAPI.DTO;

namespace AirlineAPI.Models
{
    public class Aircraft
    {
        public int AircraftID { get; set; }
        public virtual ICollection<Route> Routes { get; set; } = [];
        public string Model { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public double Range { get; set; }
        public double AverageFuelConsumption { get; set; }

        public Aircraft() { }

        public void AddRoute(Route route)
        {
            this.Routes.Add(route);
        }

    }
}
