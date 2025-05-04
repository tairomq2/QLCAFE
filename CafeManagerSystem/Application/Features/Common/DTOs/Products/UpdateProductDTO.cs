using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Common.DTOs.Products
{
    public class UpdateProductDTO
    {
        public required int ID { get; set; }
        public required string Name { get; set; }
        public required string? Description { get; set; }
        public required decimal Price { get; set; }
        public required int Stock { get; set; }
        public required string Category { get; set; }
    }
}
