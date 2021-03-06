
using System;

namespace PlaneSpotter.Models
{
   public class SpottingModel
    {
    
        public long SpotId { get; set; }
        public string Location { get; set; }
        public DateTime SpottedDateTime { get; set; }

        public bool IsDeleted { get; set; }
        public long AircraftId { get; set; }
        public string AircraftRegistration { get; set; }
        public string AircraftMake { get; set; }
        public string AircraftModel { get; set; }
        public string SpottedImageFile { get; set; }
        
    }
}
