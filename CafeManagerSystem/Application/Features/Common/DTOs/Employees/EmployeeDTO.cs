using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Common.DTOs.Employees
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Position { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public DateTime HireDate { get; set; }
        public bool IsActive { get; set; }
    }
}
