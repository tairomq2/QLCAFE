using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.User
{
    public class CreateUserDTO
    {
        public required int EmployeeId { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }

    }
}
