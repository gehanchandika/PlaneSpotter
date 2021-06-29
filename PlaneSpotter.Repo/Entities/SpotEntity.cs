using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneSpotter.Repo.Entities
{
    public class SpotEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long SpotId { get; set; }
        [MaxLength(255)]
        public string Location { get; set; }
        public DateTime SpottedDateTime { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
        public long AircraftId { get; set; }
        public string SpottedImageFile { get; set; }

        public virtual ICollection<AircraftEntity> AircraftEntity { get; set; }

    }
}
