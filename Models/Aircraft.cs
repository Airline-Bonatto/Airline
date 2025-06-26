
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

        public void Update(AircraftUpdateDTO updateData)
        {
            if(updateData.Capacity != this.Capacity)
            {
                this.Capacity = updateData.Capacity;
            }
            if(updateData.Range != this.Range)
            {
                this.Range = updateData.Range;
            }
            if(updateData.AverageFuelConsumption != this.AverageFuelConsumption)
            {
                this.AverageFuelConsumption = updateData.AverageFuelConsumption;
            }
        }

        public void AddRoute(Route route)
        {
            this.Routes.Add(route);
        }

    }
}
