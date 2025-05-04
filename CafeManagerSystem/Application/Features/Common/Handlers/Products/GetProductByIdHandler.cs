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
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ResponseApi<ProductDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetProductByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseApi<ProductDTO>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(request.Id);
            if (product == null)
                return ReponseHelper.NotFound<ProductDTO>($"Product Id {request.Id} not found");
            return new ResponseApi<ProductDTO> { Success = true, Data = product.MapToDTO(), StatusCode = 200 };
        }
    }
}
