using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Entities
{
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CityId { get; set; }

        [Required]
        [MaxLength(50)]
        public string CityName { get; set; }

        [MaxLength(500)]
        public string CityDescription { get; set; }


        //just for unit test , allow to create data using Fakers (one to many navigation)
        public IEnumerable<PointOfInterest> PointOfInterest { get; set; }

    }
}
