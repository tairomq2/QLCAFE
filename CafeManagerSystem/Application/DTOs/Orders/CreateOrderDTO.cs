using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Orders
{
    public class CreateOrderDTO
    {
        public int TableId { get; set; }
        public List<CreateOrderDetailDTO> OrderDetails { get; set; } = new();
    }
}
