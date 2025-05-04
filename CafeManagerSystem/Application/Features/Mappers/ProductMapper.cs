using Application.Features.Common.DTOs.Products;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mappers
{
    internal static class ProductMapper
    {
        internal static ProductDTO MapToDTO(this Product product)
        {
            return new ProductDTO
            {
                ID = product.ProductID,
                Name = product.ProductName,
                Description = product.Description,
                Price = product.UnitPrice,
                Stock = product.StockQuantity,
                Category = product.Category,
                IsActive = product.IsActive
            };
        }

        internal static List<ProductDTO> MapToDTOList(this IEnumerable<Product> products)
        {
            return products.Select(p => p.MapToDTO()).ToList();
        }
    }
}
