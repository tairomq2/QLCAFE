using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Orders
{
    public class CreateOrderDetailDTO
    {
        public int ProductId { get; set; }
        public required int Quantity { get; set; } = 1;
        public decimal Discount { get; set; }

    }
}
