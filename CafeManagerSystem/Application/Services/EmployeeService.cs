using Application.Interfaces;
using Application.Models;
using Core.Entities;
using Core.Interfaces;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Common.DTOs.Employees;

namespace Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public EmployeeDTO MapEmployeeToEmployeeDTO(Employee employee)
        {
            var employeeDTO = new EmployeeDTO
            {
                Id = employee.EmployeeID,
                Name = employee.FullName,
                Position = employee.Position,
                Email = employee.Email,
                Phone = employee.Phone,
                HireDate = employee.HireDate,
                IsActive = employee.IsActive
            };
            return employeeDTO;
        }
        public async Task<ResponseApi<EmployeeDTO>> CreateEmployeeAsync(CreateEmployeeDTO createEmployeeDTO)
        {
            await _unitOfWork.BeginTranSacationAsync();
            var employeeExist = await _unitOfWork.Employees.GetEmployeeBy()
                .Where(e => e.Phone == createEmployeeDTO.Phone
                || e.Email == createEmployeeDTO.Email).FirstOrDefaultAsync();
            if (employeeExist != null)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return new ResponseApi<EmployeeDTO>
                {
                    Message = $"Nhân viên đã tồn tại",
                    Success = false,
                    StatusCode = 400
                };
            }
            var employee = new Employee(createEmployeeDTO.Name, createEmployeeDTO.Position,
                createEmployeeDTO.Email, createEmployeeDTO.Phone);
            
            await _unitOfWork.Employees.AddAsync(employee);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();
            return new ResponseApi<EmployeeDTO>
            {
                Data = MapEmployeeToEmployeeDTO(employee),
                Message = "Thêm nhân viên thành công",
                Success = true,
                StatusCode = 200
            };
        }

        public async Task<ResponseApi<bool>> DeleteEmployeeAsync(int id)
        {
            await _unitOfWork.BeginTranSacationAsync();
            var employee = await _unitOfWork.Employees.GetByIdAsync(id);
            if ( employee == null)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return new ResponseApi<bool>
                {
                    Message = "Nhân viên không tồn tại",
                    Success = false,
                    StatusCode = 400,
                };
            }
                
            await _unitOfWork.Employees.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();
            return new ResponseApi<bool>
            {
                Message = "Xóa nhân viên thành công",
                StatusCode = 200,
                Success = true,
            };
            
        }

        public async Task<IEnumerable<EmployeeDTO>> GetAllEmployeeAsync()
        {
            var employees = await _unitOfWork.Employees.GetAllAsync();
            return employees.Select(e => MapEmployeeToEmployeeDTO(e));
        }

        public async Task<ResponseApi<EmployeeDTO>> GetEmployeeByIdAsync(int id)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(id);
            if (employee == null)
                return new ResponseApi<EmployeeDTO>
                {
                    Message = $"Không tìm thấy nhân viên với ID: {id}",
                    Success = false,
                    StatusCode = 404
                };
            return new ResponseApi<EmployeeDTO>
            {
                Data = MapEmployeeToEmployeeDTO(employee),
                Message = "Thành công",
                Success = true,
                StatusCode = 200
            };
        }

        public async Task<ResponseApi<EmployeeDTO>> UpdateEmployeeAsync(UpdateEmployeeDTO employeeDTO)
        {
            await _unitOfWork.BeginTranSacationAsync();
            var employee = await _unitOfWork.Employees.GetByIdAsync(employeeDTO.Id);
            if (employee == null) {
                await _unitOfWork.RollbackTransactionAsync();
                throw new Exception("Nhân viên không tồn tại"); 
            }
            employee.EmployeeUpdate(employeeDTO.Name, employeeDTO.Position, employeeDTO.Email, employeeDTO.Phone);
            await _unitOfWork.Employees.UpdateAsync(employee);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();
            return new ResponseApi<EmployeeDTO>
            {
                Data = MapEmployeeToEmployeeDTO(employee),
                Message = "Cập nhật nhân viên thành công",
                Success = true,
                StatusCode = 204
            };
        }
    }
}
