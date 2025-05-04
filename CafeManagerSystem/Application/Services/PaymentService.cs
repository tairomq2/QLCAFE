using Application.DTOs.Payment;
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
    public class PaymentService : IPaymentService
    {
        private IUnitOfWork _UnitOfWork;
        public PaymentService(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }
        public PaymentDTO MapPaymentToPaymentDTO(Payment payment)
        {
            var paymentDTO = new PaymentDTO
            {
                PaymentID = payment.PaymentID,
                InvoiceID = payment.InvoiceID,
                Amount = payment.Amount,
                PaymentMethod = payment.PaymentMethod,
                PaymentDate = payment.PaymentDate,
                TransactionCode = payment.TransactionCode,
            };
            return paymentDTO;
        }
        public async Task<ResponseApi<PaymentDTO>> ProcessPaymentAsync(ConfirmPaymentDTO confirmPayment)
        {
            await _UnitOfWork.BeginTranSacationAsync();
            try
            {
                var invoice = await _UnitOfWork.Invoices.GetInvoiceByIdAsync(confirmPayment.InvoiceID);
                if (invoice == null)
                {
                    await _UnitOfWork.RollbackTransactionAsync();
                    return new ResponseApi<PaymentDTO>
                    {
                        Success = false,
                        Message = "Invoice not found",
                        Data = null,
                        StatusCode = 404,
                    };
                }
                if (invoice.PaymentStatus != "Unpaid")
                {
                    await _UnitOfWork.RollbackTransactionAsync();
                    return new ResponseApi<PaymentDTO>
                    {
                        Success = false,
                        Message = "Invoice can't payment",
                        Data = null,
                        StatusCode = 400,
                    };
                }
                if (confirmPayment.Amount < invoice.TotalAmount)
                {
                    await _UnitOfWork.RollbackTransactionAsync();
                    return new ResponseApi<PaymentDTO>
                    {
                        Success = false,
                        Message = "Payment amout is not enough!",
                        Data = null,
                        StatusCode = 404,
                    };
                }
                //Tao payment
                var payment = new Payment(invoice.InvoiceID, invoice.TotalAmount, confirmPayment.PaymentMethod ?? "Unknow", confirmPayment.TransactionCode ?? null);
                await _UnitOfWork.Payments.AddAsync(payment);

                //Cap nhat trang thai hoa don nếu thanh toán đầy đủ
                var totalPaid = await _UnitOfWork.Payments.GetTotalPaidAmountAsync(confirmPayment.InvoiceID);
                if(totalPaid >= invoice.TotalAmount)
                {
                    invoice.UpdatePaymentStatus("Paid");
                    await _UnitOfWork.Invoices.UpdateAsync(invoice);
                }
                await _UnitOfWork.SaveChangesAsync();
                await _UnitOfWork.CommitTransactionAsync();
                return new ResponseApi<PaymentDTO>
                {
                    Success = true,
                    Message = "Payment Successful",
                    Data = MapPaymentToPaymentDTO(payment),
                    StatusCode = 200,
                };
               
            }catch(Exception ex) 
            {
                await _UnitOfWork.RollbackTransactionAsync();
                return new ResponseApi<PaymentDTO>
                {
                    Success = false,
                    Message = ex.Message,
                    Data = null,
                    StatusCode = 400,
                };
            }
        }
    }
}
