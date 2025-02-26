
using AirlineAPI.DTO;

namespace AirlineAPI.Models
{
    public class Aircraft
    {
        public int AircraftID {  get; set; }
        public virtual ICollection<Route> Routes { get; set; } = [];
        public string Model { get; set; } = string.Empty;
        public int Capacity { get; set; } 
        public double Range { get; set; }  

         public Aircraft(){}
        public Aircraft(AircraftCreateDTO createData)
        {
            this.Model = createData.Model;
            this.Capacity = createData.Capacity;
            this.Range = createData.Range;
        }

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
        }

        public void AddRoute(Route route)
        {
            this.Routes.Add(route);
        }

    }
}
