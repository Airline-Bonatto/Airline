using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineAPIV2.Models
{
    public class Route
    {
        public int RouteID { get; set; }

        public required virtual Aircraft Aircraft { get; set; }
        public required string From { get; set; }
        public required string To { get; set; }
        public double Distance { get; set; }
    }
}
