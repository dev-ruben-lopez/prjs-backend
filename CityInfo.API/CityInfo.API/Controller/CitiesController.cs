using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Controller
{
    //[Route("api/[controller]")] This would not allow later to refactor the controllers.
    [Route("api/cities")] //as all the methods in this controller are going to use this prefix route 
    public class CitiesController : ControllerBase
    {

        [HttpGet()]
        public IActionResult GetCities()
        {
            return Ok(new JsonResult(CitiesDataStore.Current.Cities));
        }

        [HttpGet("{id}")]
        public IActionResult GetCities(int id)
        {
            var city = new JsonResult(CitiesDataStore.Current.Cities.Where(c => c.Id.Equals(id)).First());
            if (city == null)
                return NotFound();
            else
                return Ok(city);
        }
            

    }
}
