using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Common.DTOs.Products
{
    public class ProductDTO
    {
        public int ID { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }

        public int Stock { get; set; }
        public string Category { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}
