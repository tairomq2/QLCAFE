using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common
{
    public static class Constants
    {
        public static Dictionary<string, decimal> RoleSalary = new Dictionary<string, decimal>
        {
            { "Quản lý", 10000000},
            { "Nhân viên thu ngân", 7000000 },
            { "Nhân viên pha chế", 6000000 },
            { "Nhân viên phục vụ", 4000000 }
        };
        public static Dictionary<int, string> RoleMappings = new Dictionary<int, string>
        {
            { 1, "Quản lý" },
            { 2, "Nhân viên phục vụ" },
            { 3, "Nhân viên pha chế" },
            { 4, "Nhân viên thu ngân" },
            { 5, "Admin"   }
        };
    }
}
