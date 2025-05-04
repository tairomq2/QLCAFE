using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Payment
    {
        [Key]
        public int PaymentID { get;private set; }
        public int InvoiceID { get; private set; }
        public decimal Amount { get; private set; }
        public string PaymentMethod { get; private set; } = string.Empty;
        public DateTime PaymentDate { get; private set; } = DateTime.Now;
        public string? TransactionCode { get; private set; }
        public Invoice? Invoice { get; private set; }

        private Payment() { }
        public Payment(int invoiceID, decimal amount, string paymentMethod, string? transactionCode = null)
        {
            InvoiceID = invoiceID;
            Amount = amount;
            PaymentMethod = paymentMethod;
            PaymentDate = DateTime.UtcNow;
            TransactionCode = transactionCode;
        }
        // Phương thức cập nhật transaction code (chỉ dùng khi thanh toán online)
        public void UpdateTransactionCode(string transactionCode)
        {
            TransactionCode = transactionCode;
        }
    }
}
