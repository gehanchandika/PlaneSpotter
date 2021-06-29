using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneSpotter.Models
{
    public class AircraftModel
    {
        public long AircraftId { get; set; }
        public string AircraftRegistration { get; set; }
        public string AircraftMake { get; set; }
        public string AircrafModel { get; set; }
    }
}
