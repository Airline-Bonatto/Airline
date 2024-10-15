using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineAPIV2.Models
{
    public class Aircraft
    {
        public int AircraftID {  get; set; }
        public virtual ICollection<Route>? Routes { get; set; } = [];
        public required string Model { get; set; }
        public int Capacity { get; set; } 
        public double Range { get; set; }  

    }
}
