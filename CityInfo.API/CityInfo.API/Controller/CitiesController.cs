using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.API.DataRepository;

namespace CityInfo.API.Controller
{
    //[Route("api/[controller]")] This would not allow later to refactor the controllers.
    [Route("api/cities")] //as all the methods in this controller are going to use this prefix route 
    public class CitiesController : ControllerBase
    {

        private ICityInfoRepository _cityInfo;

        public CitiesController(ICityInfoRepository cityInfo)
        {
            _cityInfo = cityInfo;
        }


        [HttpGet()]
        public IActionResult GetCities()
        {
            return Ok(new JsonResult(_cityInfo.GetCities()));
        }

        [HttpGet("{id}")]
        public IActionResult GetCities(int id)
        {
            var city = new JsonResult(_cityInfo.GetCity(id));
            if (city == null)
                return NotFound();
            else
                return Ok(city);
        }


            

    }
}
