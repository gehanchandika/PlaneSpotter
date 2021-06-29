using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneSpotter.Repo.Entities
{
   public class AircraftEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long AircraftId { get; set; }
        [MaxLength(8)]
        public string AircraftRegistration { get; set; }
        [MaxLength(128)]
        public string AircraftMake { get; set; }
        [MaxLength(128)]
        public string AircraftModel { get; set; }
    }
}
