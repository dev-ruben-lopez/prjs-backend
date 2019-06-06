using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Models
{
    public class CityDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public int NumberOfPointOfIntererest{ get { return PointOfIntererest.Count(); } }

        public IEnumerable<PointsOfInterestDto> PointOfIntererest { get; set; }
        = new List<PointsOfInterestDto>();
    }
}
