using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Invoices
{
    public class PrintInvoiceDTO
    {
        public int InvoiceID { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string? PaymentStatus { get; set; }
        public List<InvoiceDetailDTO> InvoiceDetails { get; set; } = new List<InvoiceDetailDTO>();
    }
}

