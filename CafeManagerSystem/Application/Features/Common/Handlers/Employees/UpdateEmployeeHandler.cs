using Application.Features.Common.Commands.Employees;
using Application.Features.Common.DTOs.Employees;
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
    public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeCommand, ResponseApi<EmployeeDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateEmployeeHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseApi<EmployeeDTO>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTranSacationAsync();
            var employee = await _unitOfWork.Employees.GetByIdAsync(request.Id);
            if (employee == null)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return ReponseHelper.NotFound<EmployeeDTO>($"Employee with {request.Id} not exist");
            }
            employee.EmployeeUpdate(request.Name, request.Position, request.Email, request.Phone);
            await _unitOfWork.Employees.UpdateAsync(employee);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();
            return ReponseHelper.Success(Mappers.EmployeeMapper.MapEmployeeToEmployeeDTO(employee), "Update Employee Successful");
        }
    }
}
