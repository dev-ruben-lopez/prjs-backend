using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controller
{

   /// <summary>
    /// Dummy controller to create and text the DB
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DUmmyController : ControllerBase
    {
        private CityDbContext _cdb;

        public DUmmyController(CityDbContext cdb)
        {
            _cdb = cdb;
        }

        [HttpGet]
        public IActionResult GetDummy()
        {
            return Ok();
        }
    }
}