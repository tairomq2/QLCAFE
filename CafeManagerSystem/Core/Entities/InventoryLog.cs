using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class InventoryLog
    {
        [Key]
        public int LogID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public DateTime LogDate { get; set; } = DateTime.Now;
        public int EmployeeID { get; set; }
        public string Note { get; set; } = string.Empty;
        public Employee? Employee { get; set; }
        public Product? Product { get; set; }

    }
}
