﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.User
{
    public class UserRoleDTO
    {
        public int RoleId { get; set; }
        public required string RoleName { get; set; }
    }
}
