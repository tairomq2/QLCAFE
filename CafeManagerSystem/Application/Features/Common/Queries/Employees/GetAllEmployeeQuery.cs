using Application.Features.Common.DTOs.Employees;
using Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Common.Queries.Employees
{
    public class GetAllEmployeeQuery : IRequest<ResponseApi<List<EmployeeDTO>>>
    {
    }
}
