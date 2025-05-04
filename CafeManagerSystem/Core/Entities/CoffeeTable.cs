using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class CoffeeTable
    {
        [Key]
        public int TableID { get; set; }
        public string TableNumber { get; set; } = string.Empty;
        public string? QRCode { get; set; }
        public string Status { get; set; } = "Available";
        public int Capacity { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
