using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class UserRole
    {
        [Key]
        public int RoleID { get; set; }
        public string RoleName { get; set; } = string.Empty;
    }
}
