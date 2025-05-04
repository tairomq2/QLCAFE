using Application.DTOs.Payment;
using Application.Interfaces;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeManagerSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentsService;
        public PaymentController(IPaymentService paymentsService)
        {
            _paymentsService = paymentsService;
        }
        [HttpPost("ConfirmPayment")]
        public async Task<IActionResult> ProcessPayment([FromBody] ConfirmPaymentDTO confirmPayment)
        {
            var result = await _paymentsService.ProcessPaymentAsync(confirmPayment);
            return Ok(result);
        }
    }
}
