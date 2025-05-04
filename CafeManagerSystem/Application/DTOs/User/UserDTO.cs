using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.User
{
    public class UserDTO
    {
        public int UserId { get;  set; }
        public int EmployeeId { get;  set; }
        public string? Username { get;  set; }
        public string? Password { get;  set; }
        public UserRoleDTO? Role { get;  set; }
        public DateTime? LastLogin { get;  set; }

    }
}
