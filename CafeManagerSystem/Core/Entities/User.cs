using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class User
    {
        [Key]
        public int UserID { get; private set; }
        public int EmployeeID { get; private set; }
        public string Username { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public int RoleID { get; private set; }
        public DateTime? LastLogin { get; private set; }
        public Employee? Employee { get; set; }
        public User(int employeeID, string username, string passwordHash, int roleID)
        {
            EmployeeID = employeeID;
            Username = username;
            PasswordHash = passwordHash;
            RoleID = roleID;
        }
        public void UpdatePassword(string newPassword)
        {
            PasswordHash = newPassword;
        }
        public void UpdateLastLogin()
        {
            LastLogin = DateTime.UtcNow;
        }
        public void UpdateRole(int newRoleID)
        {
            RoleID = newRoleID;
        }
        public string GetRoleName()
        {
            return RoleID switch
            {
                1 => "Quản lý",
                2 => "Nhân viên phục vụ",
                3 => "Nhân viên pha chế",
                4 => "Nhân viên thu ngân",
                5 => "Admin",
                _ => "Người dùng"
            };
        }
    }
}
