using Application.DTOs.Orders;
using Application.Interfaces;
using Application.Models;
using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderDetailService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public async Task<decimal> CalculateTotalAmout(List<CreateOrderDetailDTO> OrderDetails)
        {
            decimal totalAmount = 0;
            foreach (var detail in OrderDetails)
            {
                var product = await _unitOfWork.Products.GetByIdAsync(detail.ProductId);
                if (product == null)
                {
                    throw new Exception($"Sản phẩm với {detail.ProductId} không tồn tại");
                }
                decimal itemTotal = (product.UnitPrice * detail.Quantity) * (1 - detail.Discount / 100m);
                totalAmount += itemTotal;
            }
            return totalAmount;
        }
        //Tự động tạo hóa đơn chi tiết khi có khách hàng Order
        public async Task<List<OrderDetail>> CreateOrderDetailAsync(List<CreateOrderDetailDTO> orderDetailDTOs, int orderId)
        {
            var orderDetails = new List<OrderDetail>();
            foreach(var detail in orderDetailDTOs)
            {
                var product = await _unitOfWork.Products.GetByIdAsync(detail.ProductId);
                if (product == null)
                {
                    throw new Exception($"Sản phảm ID {detail.ProductId} không tồn tại");
                }
                var orderDetail = new OrderDetail(orderId, product.ProductID, detail.Quantity, product.UnitPrice, detail.Discount);
                await _unitOfWork.Orders.AddOrderDetailAsync(orderDetail);
                orderDetails.Add(orderDetail);
            }
            return orderDetails;
        }
    }
}
