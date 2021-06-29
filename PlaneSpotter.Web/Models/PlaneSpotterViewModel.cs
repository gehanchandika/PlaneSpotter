using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PlaneSpotter.Web.Models
{
    public class PlaneSpotterViewModel
    {
        [HiddenInput]
        public int SpotId { get; set; }


        [Display(Name = "Spotted Location")]
        public string Location { get; set; }
        public DateTime SpottedDateTime { get; set; }
        public string AircraftRegistration { get; set; }
        public string AircraftMake { get; set; }
        public string AircraftModel { get; set; }
    }
}
