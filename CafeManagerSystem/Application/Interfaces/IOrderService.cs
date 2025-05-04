using Application.DTOs.Orders;
using Application.Models;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDTO>> GetAllOrdersAsync();
        Task<ResponseApi<OrderDTO>> GetOrderByIdAsync(int id);
        Task<ResponseApi<OrderResponseDTO>> SendOrderToEmployee(CreateOrderDTO createOrderDTO);
        Task<ResponseApi<OrderDTO>> ConfirmOrderAsync(EmployeeConfirmOrderDTO confirmOrderDTO);
        Task<ResponseApi<OrderDTO>> CompleteOrderAsync(int orderId);
    }
}
