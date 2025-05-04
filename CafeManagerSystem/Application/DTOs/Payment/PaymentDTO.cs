using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Payment
{
    public class PaymentDTO
    {
       public required int PaymentID { get;  set; }
        public required int InvoiceID { get;  set; }
        public required decimal Amount { get;  set; }
        public required string? PaymentMethod { get;  set; }
        public DateTime PaymentDate { get;  set; }
        public  required string? TransactionCode { get;  set; }
    }
}
