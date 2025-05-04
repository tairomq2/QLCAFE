using Application.DTOs.Payment;
using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPaymentService
    {
        Task<ResponseApi<PaymentDTO>> ProcessPaymentAsync(ConfirmPaymentDTO confirmPayment);
    }
}
