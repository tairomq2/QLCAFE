using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; private set; }
        public string FullName { get;private set; } = string.Empty;
        public string Position { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string Phone { get; private set; } = string.Empty;
        public DateTime HireDate { get; private set; }
        public bool IsActive { get; private set; }
        public ICollection<Salary> Salaries { get; set; } = new List<Salary>();
        public ICollection<InventoryLog> InventoryLogs { get; set; } = new List<InventoryLog>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public Employee(string fullName, string position, string email, string phone)
        {
            FullName = fullName;
            Position = position;
            Email = email;
            Phone = phone;
            HireDate = DateTime.UtcNow;
            IsActive = true;
        }
        public void EmployeeUpdate(string fullName, string position, string email, string phone)
        {
            FullName = fullName;
            Position = position;
            Email = email;
            Phone = phone;
        }
    }
}
