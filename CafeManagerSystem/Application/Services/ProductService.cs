using Application.Interfaces;
using Application.Models;
using Core.Entities;
using Core.Interfaces;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Common.DTOs.Products;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public ProductDTO MapProductToProductDTO(Product product)
        {
            var productDTO = new ProductDTO
            {
                ID = product.ProductID,
                Name = product.ProductName,
                Description = product.Description,
                Price = product.UnitPrice,
                Stock = product.StockQuantity,
                Category = product.Category,
                IsActive = product.IsActive
            };   
            return productDTO;
        }

        public async Task<ResponseApi<ProductDTO>> CreateProductAsync(CreateProductDTO productDTO)
        {
            await _unitOfWork.BeginTranSacationAsync();
            try
            {
                var productExist = await _unitOfWork.Products.GetProductBy()
               .Where(p => p.ProductName == productDTO.Name
               ).FirstOrDefaultAsync();
                if (productExist != null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return new ResponseApi<ProductDTO>
                    {
                        Message = $"Sản phẩm đã tồn tại",
                        Success = false,
                        StatusCode = 400
                    };
                }
                var product = new Product(productDTO.Name, productDTO.Description, productDTO.Price, productDTO.Stock, productDTO.Category);
                await _unitOfWork.Products.AddAsync(product);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
                return new ResponseApi<ProductDTO>
                {
                    Message = $"Thêm sản phẩm thành công",
                    Success = true,
                    Data = MapProductToProductDTO(product),
                    StatusCode = 200
                };
            }
            catch(Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return new ResponseApi<ProductDTO>
                {
                    Message = ex.Message
                };
            }
        }
        
        public async Task DeleteProductAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null)
               throw new Exception($"Sản phẩm không tồn tại");
            await _unitOfWork.Products.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }


        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
           var products = await _unitOfWork.Products.GetAllAsync();
           return products.Select(p => MapProductToProductDTO(p));
        }

        public async Task<ResponseApi<ProductDTO>> GetProductByIdAsync(int id)
        {
           var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null)
                return new ResponseApi<ProductDTO> {Success = false, Message = $"Không tìm thấy sản phẩm với ID: {id}" , StatusCode = 404};
            return new ResponseApi<ProductDTO> { Success = true, Data = MapProductToProductDTO(product), StatusCode = 200};
        }

        public async Task<ResponseApi<ProductDTO>> UpdateProductAsync(UpdateProductDTO productDTO)
        {
            await _unitOfWork.BeginTranSacationAsync();
            var product = await _unitOfWork.Products.GetByIdAsync(productDTO.ID);
            if (product == null)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return new ResponseApi<ProductDTO>
                {
                    Success = false,
                    Message = "Product does not exist",
                    StatusCode = 404
                };
            }         
            product.UpdateProduct(productDTO.Name, productDTO.Description, productDTO.Price, productDTO.Stock, productDTO.Category);
            await _unitOfWork.Products.UpdateAsync(product);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();
            return new ResponseApi<ProductDTO>
            {
                Success = true,
                Message = "Update product succesfully",
                Data = MapProductToProductDTO(product),
                StatusCode = 204
            };
        }
       
    }
}
