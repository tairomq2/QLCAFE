using Application.Features.Common.Commands.Employees;
using Application.Helpers;
using Application.Models;
using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Common.Handlers.Employees
{
    public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeCommand, ResponseApi<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteEmployeeHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseApi<bool>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTranSacationAsync();
            var employee = await _unitOfWork.Employees.GetByIdAsync(request.EmployeeId);
            if (employee == null)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return ReponseHelper.NotFound<bool>("Employee does not exist");
            }

            await _unitOfWork.Employees.DeleteAsync(request.EmployeeId);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();
            return ReponseHelper.Success(true, "Delete Employee Successful");

        }
    }
}
