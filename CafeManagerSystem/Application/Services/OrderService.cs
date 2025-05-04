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
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderDetailService _orderDetailService;
        private readonly IInvoiceService _invoiceService;
        public OrderService(IUnitOfWork unitOfWork, IOrderDetailService orderDetailService, IInvoiceService invoiceService)
        {
            _unitOfWork = unitOfWork;
            _orderDetailService = orderDetailService;
            _invoiceService = invoiceService;
        }
        public OrderDTO MapOrderToOrderDTO(Order order)
        {
        var orderDTO = new OrderDTO
            {
                OrderID = order.OrderID,
                TableID = order.TableID,
                EmployeeID = order.EmployeeID > 0 ? order.EmployeeID : null, // Kiểm tra nếu 0 thì bỏ qua
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
            };
            return orderDTO;
        }
        public OrderResponseDTO MapOrderDTOToOrderResponseDTO(OrderDTO orderDTO)
        {
            return new OrderResponseDTO
            {
                OrderID = orderDTO.OrderID,
                TableID = orderDTO.TableID,
                OrderDate = orderDTO.OrderDate,
                TotalAmount = orderDTO.TotalAmount,
                Status = orderDTO.Status
            };
        }

        public async Task<ResponseApi<OrderDTO>> ConfirmOrderAsync(EmployeeConfirmOrderDTO confirmOrderDTO)
        {
            await _unitOfWork.BeginTranSacationAsync();
            try
            {
                var order = await _unitOfWork.Orders.GetByIdAsync(confirmOrderDTO.OrderID);
                var employee = await _unitOfWork.Employees.GetByIdAsync(confirmOrderDTO.EmployeeID);
                //Chỉ có nhân viên pha chế mới chấp nhận Order
                if(employee?.Position != "Nhân viên pha chế")
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return new ResponseApi<OrderDTO>()
                    {
                        Success = false,
                        Message = "This employee can't confirm order",
                        Data = null,
                    };
                }
                //Nếu như đơn hàng k tồn tại
                if (order == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return new ResponseApi<OrderDTO>()
                    {
                        Success = false,
                        Message = "Order does not exist",
                        Data = null,
                    };    
                }
                //Nếu như đơn hàng không ở trong trạng thái pending
                if(order.Status != "Pending")
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return new ResponseApi<OrderDTO>()
                    {
                        Success = false,
                        Message = "Order isn't in pending status",
                        Data = null,
                    };
                }
                //Cập nhật EmployeeId và trạng thái vào đơn Order
                order.ConfirmOrder(confirmOrderDTO.EmployeeID);
                //Cập nhật vào DB
                await _unitOfWork.Orders.UpdateAsync(order);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
                return new ResponseApi<OrderDTO>
                {
                    Success = true,
                    Message = "Order confirmed succesfully",
                    Data = MapOrderToOrderDTO(order),
                };
            }catch(Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return new ResponseApi<OrderDTO>()
                {
                    Success = false,
                    Message = ex.Message,
                    Data = null,
                };
            }
        }
        public async Task<ResponseApi<OrderDTO>> CompleteOrderAsync(int orderId)
        {
            await _unitOfWork.BeginTranSacationAsync();
            try
            {
                var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
                if(order == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return new ResponseApi<OrderDTO>()
                    {
                        Success = false,
                        Message = "Order does not exist",
                        Data = null
                    };
                }
                if(order.Status != "Confirmed")
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return new ResponseApi<OrderDTO>()
                    {
                        Success = false,
                        Message = "Order isn't in confirmed status",
                        Data = null
                    };
                }
                //Cập nhật trạng thái Order
                order.ChangeCompletedStatus();
                //Lưu vào DB
                await _unitOfWork.Orders.UpdateAsync(order);
                await _unitOfWork.SaveChangesAsync();
                //Tự động tạo hóa đơn
                var invoice = await _invoiceService.CreateInvoiceAsync(order.OrderID);
                await _unitOfWork.CommitTransactionAsync();
                return new ResponseApi<OrderDTO>
                {
                    Success = true,
                    Message = "Order Completed & Invoice created successfully!",
                    Data = MapOrderToOrderDTO(order)
                };
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return new ResponseApi<OrderDTO>() { Success = false, Message = ex.Message, Data = null };
            }
        }
        public async Task<IEnumerable<OrderDTO>> GetAllOrdersAsync()
        {
            var orders = await _unitOfWork.Orders.GetAllAsync();
            return orders.Select(o => MapOrderToOrderDTO(o));
        }

        public async Task<ResponseApi<OrderDTO>> GetOrderByIdAsync(int id)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(id);
            if (order == null)
            {
                return new ResponseApi<OrderDTO> { Success = false, Message = "Không tìm thấy dữ liệu Order", StatusCode = 404, Data = null };
            }
            return new ResponseApi<OrderDTO> { Success = true, StatusCode = 200, Data = MapOrderToOrderDTO(order) };
        }

        public async Task<ResponseApi<OrderResponseDTO>> SendOrderToEmployee(CreateOrderDTO createOrderDTO)
        {
            await _unitOfWork.BeginTranSacationAsync();
            try
            {
                //tính tổng tiền từ OrderDetail
                var totalAmount = await _orderDetailService.CalculateTotalAmout(createOrderDTO.OrderDetails);
                //Tạo order
                var order = new Order(createOrderDTO.TableId, null, totalAmount, "Pending");

                await _unitOfWork.Orders.AddAsync(order);
                await _unitOfWork.SaveChangesAsync();
                //Tao danh sach OrderDetail
                var orderDetail = await _orderDetailService.CreateOrderDetailAsync(createOrderDTO.OrderDetails, order.OrderID);

                await _unitOfWork.CommitTransactionAsync();
                return new ResponseApi<OrderResponseDTO>
                {
                    Success = true,
                    Message = "Order created successfully",
                    Data = MapOrderDTOToOrderResponseDTO(MapOrderToOrderDTO(order))

                };

            }
            catch(Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return new ResponseApi<OrderResponseDTO>
                {
                    Success = false,
                    Message = ex.Message,
                    Data = null,
                };
            }
        }
    }
}
