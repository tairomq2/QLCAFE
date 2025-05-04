using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Orders
{
    public class OrderDTO
    {
        public int OrderID { get; set; }
        public int TableID { get; set; }
        public int? EmployeeID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public required string Status { get; set; }
        public List<OrderDetailDTO> OrderDetails { get; set; } = new();

    }
}
