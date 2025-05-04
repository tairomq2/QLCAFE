using Application.Features.Common.Commands.Employees;
using Application.Features.Common.DTOs.Employees;
using Application.Helpers;
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

namespace Application.Features.Common.Handlers.Employees
{
    public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, ResponseApi<EmployeeDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateEmployeeHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseApi<EmployeeDTO>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTranSacationAsync();
            var employeeExist = await _unitOfWork.Employees.GetEmployeeBy()
                .Where(e => e.Phone == request.Phone
                || e.Email == request.Email).FirstOrDefaultAsync();
            if (employeeExist != null)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return ReponseHelper.Fail<EmployeeDTO>("Employee already exist");
            }
            var employee = new Employee(request.Name, request.Position,
                request.Email, request.Phone);

            await _unitOfWork.Employees.AddAsync(employee);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();
            return ReponseHelper.Success(Mappers.EmployeeMapper.MapEmployeeToEmployeeDTO(employee),"Add Employee successful");
        }
    }
}
