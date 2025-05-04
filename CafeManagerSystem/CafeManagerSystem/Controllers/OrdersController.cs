using Application.DTOs.Orders;
using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeManagerSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        IOrderService _orderService;
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("Order")]
        public async Task<IActionResult> AddOrderFromCustomerToEmployee(CreateOrderDTO createOrderDTO)
        {
            await _orderService.SendOrderToEmployee(createOrderDTO);
            return Ok(createOrderDTO);
        }
        [HttpGet("GetOrder")]
        public async Task<IActionResult> GetOrder()
        {
            var order = await _orderService.GetAllOrdersAsync();
            return Ok(order);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            return Ok(order);
        }
        [HttpPost("ConfirmOrder")]
        public async Task<IActionResult> ConfirmOrder([FromBody] EmployeeConfirmOrderDTO employeeConfirmOrderDTO)
        {
            var cfOrder = await _orderService.ConfirmOrderAsync(employeeConfirmOrderDTO);
            return Ok(cfOrder);
        }
        [HttpPut("CompleteOrder/{id}")]
        public async Task<IActionResult> CompleteOrder(int id)
        {
            var cOrder = await _orderService.CompleteOrderAsync(id);
            return Ok(cOrder);
        }
    }
}
