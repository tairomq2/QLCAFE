using Application.Features.Common.DTOs.Products;
using Application.Features.Common.Queries.Products;
using Application.Features.Mappers;
using Application.Helpers;
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
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, ResponseApi<List<ProductDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllProductsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseApi<List<ProductDTO>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTranSacationAsync();
            try
            {
                var products = await _unitOfWork.Products.GetAllAsync();
                if (products == null || !products.Any())
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return ReponseHelper.NotFound<List<ProductDTO>>("Not have products");
                }
                var productDtos = products.Select(p => p.MapToDTO()).ToList();
                await _unitOfWork.CommitTransactionAsync();
                return ReponseHelper.Success(productDtos, "Get list product completed");
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                return ReponseHelper.ServerError<List<ProductDTO>>("Error! Get all product");
            }
        }
    }
}
