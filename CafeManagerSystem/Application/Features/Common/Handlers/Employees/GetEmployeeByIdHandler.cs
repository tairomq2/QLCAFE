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
    public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeByIdQuery, ResponseApi<EmployeeDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetEmployeeByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseApi<EmployeeDTO>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTranSacationAsync();
            try
            {
                var employee = await _unitOfWork.Employees.GetByIdAsync(request.EmployeeId);
                if (employee == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return ReponseHelper.NotFound<EmployeeDTO>($"Không tìm thấy nhân viên với ID: {request.EmployeeId}");
                }
                await _unitOfWork.CommitTransactionAsync();
                return ReponseHelper.Success(Mappers.EmployeeMapper.MapEmployeeToEmployeeDTO(employee),"Get employee sucessful");
            }catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return ReponseHelper.BadRequest<EmployeeDTO>(ex.Message);
            }
            
        }
    }
}
