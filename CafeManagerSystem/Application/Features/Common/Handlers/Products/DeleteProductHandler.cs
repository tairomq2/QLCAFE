using Application.Features.Common.Commands;
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
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, ResponseApi<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteProductHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseApi<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTranSacationAsync();
            try
            {
                var product = await _unitOfWork.Products.GetByIdAsync(request.ProductId);
                if (product == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return ReponseHelper.NotFound<bool>("Product doesn't exist");
                }
                await _unitOfWork.Products.DeleteAsync(request.ProductId);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
                return ReponseHelper.Success(true, "Delete product sucessful");
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                return ReponseHelper.ServerError<bool>("Eror! Delete product");
            }
        }
    }
}
