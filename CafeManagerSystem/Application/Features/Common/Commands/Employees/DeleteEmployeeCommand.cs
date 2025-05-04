using Application.Features.Common.DTOs.Employees;
using Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Common.Commands.Employees
{
    public class DeleteEmployeeCommand : IRequest<ResponseApi<bool>>
    {
        public int EmployeeId { get; set; }
        public DeleteEmployeeCommand(int employeeId) => EmployeeId = employeeId;
    }
}
