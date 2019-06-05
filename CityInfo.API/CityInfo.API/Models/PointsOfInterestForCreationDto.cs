using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

/*
 Some development notes :

    The validation in the model, and the error messages are ok if using only here. But
    in many cases these validations are not so simple. You will end writing validations in code
    directly in the controller, so you could end having validations in two different places: model and controller
    which is not appropiate and is violating the separation of concerns...... which make me think
    into create a validator class for model or controller perhaps....or even better use FluentValidations compoent
    from Nugget Pakg.
     */

namespace CityInfo.API.Models
{
    public class PointsOfInterestForCreationDto
    {

        [Required(ErrorMessage ="Please provide a name for the point of interest.")]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
    }
}
