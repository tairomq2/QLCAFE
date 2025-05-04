using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Salary
    {
        [Key]
        public int SalaryID { get; set; }
        public int EmployeeID { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal Bonus { get; set; } = 0;
        public decimal TotalAmount { get; set; }
        public Employee? Employee { get; set; }
    }
}
