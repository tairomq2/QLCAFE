using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Inventory
    {
        [Key]
        public int InventoryID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public DateTime LastUpdated { get; set; } = DateTime.Now;
        public Product? Product { get; set; }
    }
}
