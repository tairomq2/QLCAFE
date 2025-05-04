using Application.Features.Common.Commands;
using Application.Features.Common.DTOs.Products;
using Application.Features.Mappers;
using Application.Models;
using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Common.Handlers.Products
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, ResponseApi<ProductDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateProductHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseApi<ProductDTO>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTranSacationAsync();
            var product = await _unitOfWork.Products.GetByIdAsync(request.ID);
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
            product.UpdateProduct(request.Name, request.Description, request.Price, request.Stock, request.Category);
            await _unitOfWork.Products.UpdateAsync(product);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();
            return new ResponseApi<ProductDTO>
            {
                Success = true,
                Message = "Update product succesfully",
                Data = product.MapToDTO(),
                StatusCode = 204
            };
        }
    }
}

