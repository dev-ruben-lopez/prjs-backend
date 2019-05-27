using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Acrodef.API.v1.Controllers
{
    [Route("api/v1/definition")]
    public class DefinitionController : ControllerBase
    {

        [HttpGet("{id}")]
        public IActionResult Get(int definitionId)
        {
            return Ok("Value");
        }


        [HttpGet("search")]
        public IActionResult SearchDefinition(string searchFor)
        {
            if (string.IsNullOrWhiteSpace(searchFor))
                return BadRequest("No value to search");

            var result = new List<string>() { "result1", "result2" };
            return Ok(result);
        }
    }
}
