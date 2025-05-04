using Application.DTOs.Invoices;
using Application.Interfaces;
using Application.Models;
using Core.Entities;
using Core.Interfaces;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        public InvoiceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public InvoiceDTO MapInvoiceToInvoiceDTO(Invoice invoice)
        {
            var invoiceDTO = new InvoiceDTO
            {
                InvoiceID = invoice.InvoiceID,
                OrderID = invoice.OrderID,
                InvoiceDate = invoice.InvoiceDate,
                TotalAmount = invoice.TotalAmount,
                PaymentStatus = invoice.PaymentStatus,
            };
            return invoiceDTO;
        }

        public async Task<InvoiceDTO> CreateInvoiceAsync(int orderId)
        {
            await _unitOfWork.BeginTranSacationAsync();
            try
            {
                var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
                if (order == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    throw new Exception($"Order ID {orderId} không tồn tại!");
                }
                if(order.Status != "Completed")
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    throw new Exception("Invoices can only be created in the completed status");
                }
                var invoice = new Invoice(orderId, order.TotalAmount);

                await _unitOfWork.Invoices.AddAsync(invoice);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
                return new InvoiceDTO
                {
                    InvoiceID = invoice.InvoiceID,
                    OrderID = invoice.OrderID,
                    InvoiceDate = invoice.InvoiceDate,
                    TotalAmount = invoice.TotalAmount,
                    PaymentStatus = invoice.PaymentStatus
                };
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<IEnumerable<InvoiceDTO>> GetAllInvoiceAsync()
        {
            var invoices = await _unitOfWork.Invoices.GetAllAsync();
            return invoices.Select(i => MapInvoiceToInvoiceDTO(i));
        }

        public async Task<PrintInvoiceDTO?> GetInvoiceByIdAsync(int id)
        {
            var invoice = await _unitOfWork.Invoices.GetInvoiceBy().
                Where(i => i.InvoiceID == id)
                .Include(i => i.Order)
                .ThenInclude(o => o!.OrderDetails)
                .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync();
            if (invoice == null)
            {
                throw new Exception("Invoice does not exist");
            }
            if (invoice.Order == null)
            {
                throw new Exception("Order is missing!");
            }

            //Mapping du lieu vao DTO
            var invoiceDTO = new PrintInvoiceDTO
            {
                InvoiceID = invoice.InvoiceID,
                InvoiceDate = invoice.InvoiceDate,
                TotalAmount = invoice.TotalAmount,
                PaymentStatus = invoice.PaymentStatus,
                InvoiceDetails = invoice.Order.OrderDetails.Select(od => new InvoiceDetailDTO
                {
                    ProductName = od.Product?.ProductName ?? "Unknow",
                    Quantity = od.Quantity,
                    UnitPrice = od.UnitPrice
                }).ToList(),
            };
            return invoiceDTO;
        }
    }
}
