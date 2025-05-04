using Application.Features.Common.DTOs.Products;
using Application.Models;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
        Task<ResponseApi<ProductDTO>> GetProductByIdAsync(int id);
        Task<ResponseApi<ProductDTO>> CreateProductAsync(CreateProductDTO productDTO);
        Task<ResponseApi<ProductDTO>> UpdateProductAsync(UpdateProductDTO productDTO);
        Task DeleteProductAsync(int id);
    }
}
