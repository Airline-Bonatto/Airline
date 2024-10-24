
using AirlineAPI.DTO;

namespace AirlineAPIV2.Models
{
    public class Aircraft
    {
        public int AircraftID {  get; set; }
        public virtual ICollection<Route>? Routes { get; set; } = [];
        public required string Model { get; set; }
        public int Capacity { get; set; } 
        public double Range { get; set; }  



        public void update(AircraftUpdateDto updateData)
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

    }
}
