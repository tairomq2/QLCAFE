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
    public interface IOrderDetailService
    {
        Task<List<OrderDetail>> CreateOrderDetailAsync(List<CreateOrderDetailDTO> orderDetailDTOs, int orderId);
        Task<decimal> CalculateTotalAmout(List<CreateOrderDetailDTO> createOrderDetails);
    }
}
