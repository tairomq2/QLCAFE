using Application.Features.Common.Commands.Employees;
using Application.Features.Common.DTOs.Employees;
using Application.Features.Common.Queries.Employees;
using Application.Interfaces;
using Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeManagerSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : Controller
    {
        private readonly IMediator _mediator;
        public EmployeesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetAllEmployee()
        {
            var employees = await _mediator.Send(new GetAllEmployeeQuery());
            return Ok(employees);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployeeById(int id)
        {
            var employee = await _mediator.Send(new GetEmployeeByIdQuery(id));
            return Ok(employee);
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] CreateEmployeeCommand command)
        {
            var createdEmployee = await _mediator.Send(command);
            return Ok(createdEmployee);
        }
        [HttpPut("Update")]
        public async Task<ActionResult<EmployeeDTO>> UpdateEmployee([FromBody] UpdateEmployeeCommand command)
        {
            var updatedEmployee = await _mediator.Send(command);
            return Ok(updatedEmployee);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
             var deleteEmployee =  await _mediator.Send(new DeleteEmployeeCommand(id));
            return Ok(deleteEmployee);
        }
    }
}
