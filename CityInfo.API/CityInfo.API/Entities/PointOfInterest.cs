using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Entities
{
    public class PointOfInterest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PointId { get; set; }

        [Required]
        [MaxLength(50)]
        public string PointName { get; set; }

        [MaxLength(500)]
        public string PointDescription { get; set; }

        [MaxLength(500)]
        public string PointWikipediaLink { get; set; }

        [ForeignKey("CityId")]
        public City City { get; set; }

        public int CityId { get; set; }
    }
}
