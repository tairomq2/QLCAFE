using Application.Features.Common.DTOs.Employees;
using Application.Features.Common.Queries.Employees;
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
    public class GetAllEmployeeHandler : IRequestHandler<GetAllEmployeeQuery, ResponseApi<List<EmployeeDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllEmployeeHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseApi<List<EmployeeDTO>>> Handle(GetAllEmployeeQuery request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTranSacationAsync();
            try
            {
                var employees = await _unitOfWork.Employees.GetAllAsync();
                if (employees == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return ReponseHelper.NotFound<List<EmployeeDTO>>("No employees found");
                }
                return ReponseHelper.Success(Mappers.EmployeeMapper.MapToDTOList(employees),"Get All Employee Successful");
            }
            catch (Exception ex)
            {
                 await _unitOfWork.RollbackTransactionAsync();
                return ReponseHelper.BadRequest<List<EmployeeDTO>>(ex.Message);
            }

        }
    }
}
