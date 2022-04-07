using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Dtos
{
    //It may look similar to class Item, and it should, but in the future this could be helpful.
    public record ItemsDto
    {
        public Guid id { get; init; }
        public string Name { get; init; }
        public decimal Price { get; init; }
        public DateTimeOffset CreatedDate { get; init; }

    }
}
