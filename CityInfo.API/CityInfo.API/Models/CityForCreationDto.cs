using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Models
{
    public class CityForCreationDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public int NumberOfPointOfIntererest{ get { return PointOfIntererest.Count(); } }

        public IEnumerable<PointsOfInterestForCreationDto> PointOfIntererest { get; set; }
        = new List<PointsOfInterestForCreationDto>();
    }
}
