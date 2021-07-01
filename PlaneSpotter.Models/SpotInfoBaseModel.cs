using System;

namespace PlaneSpotter.Models
{
    public class SpotInfoBaseModel
    {
        public int SpotId { get; set; }
        public string Location { get; set; }
        public DateTime SpottedDateTime { get; set; }
        public bool IsDeleted { get; set; }
        public long AircraftId { get; set; }
    }
}
