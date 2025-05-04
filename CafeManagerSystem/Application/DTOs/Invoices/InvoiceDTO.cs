using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Invoices
{
    public class InvoiceDTO
    {
        public required int InvoiceID { get;  set; }
        public required int OrderID { get;  set; }
        public required DateTime InvoiceDate { get;  set; }
        public required decimal TotalAmount { get;  set; }
        public required string PaymentStatus { get;  set; }
    }
}
