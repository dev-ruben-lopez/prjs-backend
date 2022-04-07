using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Repositories;
using Catalog.Entities;
using Catalog.Dtos;

namespace Catalog.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {

        private readonly IItemsRepository repository;

        public ItemsController(IItemsRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IEnumerable<ItemsDto> GetItems()
        {
            var items = repository.GetItems().Select(item => item.asDto() );
            return items;
        }


        [HttpGet("{id}")]
        public ActionResult<ItemsDto> getItem(Guid id)
        {
            var item = repository.GetItem(id);

            if(item == null) { return NotFound(); }

            return item.asDto();
        }

    }
}
