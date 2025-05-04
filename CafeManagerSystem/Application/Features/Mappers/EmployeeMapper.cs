using Application.Features.Common.DTOs.Employees;
using Application.Features.Common.DTOs.Products;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mappers
{
    internal static class EmployeeMapper
    {
        internal static EmployeeDTO MapEmployeeToEmployeeDTO(Employee employee)
        {
            return new EmployeeDTO
            {
                Id = employee.EmployeeID,
                Name = employee.FullName,
                Position = employee.Position,
                Email = employee.Email,
                Phone = employee.Phone,
                HireDate = employee.HireDate,
                IsActive = employee.IsActive
            };
        }
        internal static List<EmployeeDTO> MapToDTOList(this IEnumerable<Employee> employees)
        {
            return employees.Select(e => MapEmployeeToEmployeeDTO(e)).ToList();
        }

    }
}
