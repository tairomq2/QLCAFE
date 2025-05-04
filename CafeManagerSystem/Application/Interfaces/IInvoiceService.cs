using Application.DTOs.Invoices;
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
    public interface IInvoiceService
    {
        Task<IEnumerable<InvoiceDTO>> GetAllInvoiceAsync();
        Task<InvoiceDTO> CreateInvoiceAsync(int orderId);
        Task<PrintInvoiceDTO?> GetInvoiceByIdAsync(int id);
        
    }
}
