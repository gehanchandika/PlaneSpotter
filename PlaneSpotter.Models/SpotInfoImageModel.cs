using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneSpotter.Models
{
    public class SpotInfoImageModel
    {
        public long ImageId { get; set; }
        public long SpotId { get; set; }
        public string SpotAircraftImage { get; set; }
    }
}
