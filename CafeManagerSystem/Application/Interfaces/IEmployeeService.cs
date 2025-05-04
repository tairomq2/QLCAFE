using Application.Features.Common.DTOs.Employees;
using Application.Models;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDTO>> GetAllEmployeeAsync();
        Task<ResponseApi<EmployeeDTO>> GetEmployeeByIdAsync(int id);
        Task<ResponseApi<EmployeeDTO>> CreateEmployeeAsync(CreateEmployeeDTO createEmployeeDTO);
        Task<ResponseApi<EmployeeDTO>> UpdateEmployeeAsync(UpdateEmployeeDTO employeeDTO);
        Task<ResponseApi<bool>> DeleteEmployeeAsync(int id);
    }
}
