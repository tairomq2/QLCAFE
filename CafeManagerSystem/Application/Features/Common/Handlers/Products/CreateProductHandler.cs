using Application.Features.Common.Commands;
using Application.Features.Common.DTOs.Products;
using Application.Features.Mappers;
using Application.Models;
using Core.Entities;
using Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Common.Handlers.Products
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, ResponseApi<ProductDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateProductHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseApi<ProductDTO>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTranSacationAsync();
            try
            {
                var productExist = await _unitOfWork.Products.GetProductBy()
               .Where(p => p.ProductName == request.Name
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
                var product = new Product(request.Name ?? "", request.Description, request.Price, request.Stock, request.Category ?? "");
                await _unitOfWork.Products.AddAsync(product);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
                return new ResponseApi<ProductDTO>
                {
                    Message = $"Thêm sản phẩm thành công",
                    Success = true,
                    Data = product.MapToDTO(),
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return new ResponseApi<ProductDTO>
                {
                    Message = ex.Message
                };
            }
        }
    }
}
