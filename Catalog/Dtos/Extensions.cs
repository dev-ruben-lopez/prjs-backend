using Catalog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Dtos
{
    public static class Extensions
    {
        public static ItemsDto asDto(this Item item)
        {
            return new ItemsDto
            {
                id = item.id,
                Name = item.Name,
                Price = item.Price,
                CreatedDate = item.CreatedDate
            };
        }
    }
}
