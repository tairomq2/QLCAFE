using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Order> AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            return order;
        }

        public async Task<OrderDetail> AddOrderDetailAsync(OrderDetail orderDetail)
        {
            await _context.OrderDetails.AddAsync(orderDetail);
            return orderDetail;
        }
        public async Task<OrderDetail?> GetOrderDetailByIdAsync(int id)
        {
            return await _context.OrderDetails.FirstOrDefaultAsync(o => o.OrderDetailID == id);
        }

        public Task<Order> CancelOrderAsync(int orderId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _context.Orders.Include(x => x.OrderDetails).FirstOrDefaultAsync(x => x.OrderID == id);
        }

        public async Task<IEnumerable<Order>> GetStatusOrderAsync()
        {
            return await _context.Orders.Include(x => x.OrderDetails).Where(x => x.Status == "Pending").ToListAsync();
        }

        public Task UpdateAsync(Order entity)
        {
            _context.Orders.Update(entity);
            return Task.CompletedTask;
        }
    }
}
