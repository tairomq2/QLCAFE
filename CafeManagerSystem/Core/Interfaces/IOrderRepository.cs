using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<OrderDetail> AddOrderDetailAsync(OrderDetail orderDetail);
        Task<Order> CancelOrderAsync(int  orderId);
        Task<IEnumerable<Order>> GetStatusOrderAsync();
        Task<OrderDetail?> GetOrderDetailByIdAsync(int id);
    }
}
