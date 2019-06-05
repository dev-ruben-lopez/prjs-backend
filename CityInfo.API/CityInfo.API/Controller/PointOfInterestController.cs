using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Controller
{
    [Route("api/cities")]
    public class PointOfInterestController : ControllerBase
    {

        //props and objects to inject
        private IMailService _mailService;
        private ILogger<PointOfInterestController> _logger;


        public PointOfInterestController(IMailService mailService, ILogger<PointOfInterestController> logger)
        {
            _mailService = mailService;
            _logger = logger;

            _logger.LogDebug("Blah !!!");
            _logger.Log(LogLevel.Information, "Bummm!! .. I've just injected ILogger to this controller !!!");
        }



        [HttpGet("{cityId}/pointofinterest")]
        public IActionResult GetPointOfInterest(int cityId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
                return NotFound();

            return Ok(city.PointOfIntererest);
        }

        [HttpGet("{cityId}/pointofinterest/{id}")]
        public IActionResult GetPointOfInterest(int cityId, int id)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
                return NotFound("City");

            var point = city.PointOfIntererest.FirstOrDefault(c => c.Id == id);
            if (point == null)
                return NotFound("PointOfIntererest");

            return Ok(point);
        }

        [HttpPost("{cityId}/pointofinterest")]
        public IActionResult CreatePointOfInterest(int cityId, [FromBody] PointsOfInterestForCreationDto pointOfInterest)
        {
            if (pointOfInterest == null)
                return BadRequest(ModelState);

            var city = CitiesDataStore.Current.Cities.First(c => c.Id == cityId);
            if (city == null)
                return BadRequest();

            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            //mock thr response, as is in memory data

            var maxPointOfInterestId = CitiesDataStore.Current.Cities.SelectMany(
                c => c.PointOfIntererest).Max(p => p.Id);

            //create the resource
            PointsOfInterestDto newPointOfInterest = new PointsOfInterestDto() {
                Id = maxPointOfInterestId++,
                Description = pointOfInterest.Description,
                Name = pointOfInterest.Name
            };

            return CreatedAtRoute("GetPointOfInterest", new { cityId = cityId, pointOfInterest = newPointOfInterest });
        }

        [HttpPut("{cityId}/pointofinterest/{id}")]
        public IActionResult UpdatePointOfInterest(int cityId, int id, [FromBody] PointsOfInterestForCreationDto pointOfInterest)
        {

            if (pointOfInterest == null)
                return BadRequest(ModelState);

            var city = CitiesDataStore.Current.Cities.First(c => c.Id == cityId);
            if (city == null)
            {
                ModelState.AddModelError("", "City Id not found");
                return BadRequest(ModelState);
            }


            var currentPointOfInterest = CitiesDataStore.Current.Cities.SelectMany(c => c.PointOfIntererest).FirstOrDefault(p => p.Id == id);
            if (currentPointOfInterest == null)
            {
                ModelState.AddModelError("", "Point Id not found");
                return BadRequest(ModelState);
            }


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //update
            currentPointOfInterest.Name = pointOfInterest.Name;
            currentPointOfInterest.Description = pointOfInterest.Description;

            return NoContent();
        }


        /// <summary>
        /// Implementation of PATCH for the PointOfInterest object
        /// NOTE : in the original example, it should work with PointOfInterestForUpdateDto, but is not, so using directly PointOfInterestDto
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="id"></param>
        /// <param name="patchDoc"></param>
        /// <returns></returns>
        [HttpPatch("{cityId}/pointofinterest/{id}")]
        public IActionResult PartialUpdatePointOfInterest(int cityId, int id,[FromBody] JsonPatchDocument<PointsOfInterestDto> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest(ModelState);

            var city = CitiesDataStore.Current.Cities.First(c => c.Id == cityId);
            if (city == null)
            {
                ModelState.AddModelError("", "City Id not found");
                return BadRequest(ModelState);
            }



            var pointOfInterestFromDB = city.PointOfIntererest.FirstOrDefault(p => p.Id == id);
            if (pointOfInterestFromDB == null)
            {
                ModelState.AddModelError("", "Point Id not found");
                return BadRequest(ModelState);
            }

            /*
            var pointOfInterestToPatch = new PointsOfInterestForCreationDto() {
                Name = pointOfInterestFromDB.Name,
                Description = pointOfInterestFromDB.Description
            };
            */

            patchDoc.ApplyTo(pointOfInterestFromDB, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);




            return NoContent();


        }



        [HttpDelete("{cityId}/pointofinterest/{id}")]
        public IActionResult DeletePointOfInterest(int cityId, int id)
        {
            _logger.LogInformation("Hello, this is DeletePointOfInterest");
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if(city == null)
            {
                _logger.LogError("City not found for id " + cityId);
                return BadRequest("City not found");
            }

            var pointOfInterest = city.PointOfIntererest.FirstOrDefault(p => p.Id == id);
            if (pointOfInterest == null)
            {
                _logger.LogError("Point of interest not found for id " + id);
                return BadRequest("Point not found");
            }

            pointOfInterest = null; // method .Remove() not available .... ???

            _mailService.Send("Resource Deletetion", "Item " + id + " deleted");
 
            return NoContent();
        }
    }
}
