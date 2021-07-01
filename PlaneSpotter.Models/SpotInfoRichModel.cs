using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneSpotter.Models
{
    public class SpotInfoRichModel
    {
        public long SpotId { get; set; }
        public string Location { get; set; }
        public DateTime SpottedDateTime { get; set; }
        public bool IsDeleted { get; set; }
        public long AircraftId { get; set; }
        public string AircraftRegistration { get; set; }
        public string AircraftMake { get; set; }
        public string AircraftModel { get; set; }
        public string SpotAircraftImage { get; set; }
    }
}
