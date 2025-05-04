using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Invoice
    {
        [Key]
        public int InvoiceID { get;private set; }
        public int OrderID { get; private set; }
        public DateTime InvoiceDate { get; private set; } = DateTime.Now;
        public decimal TotalAmount { get; private set; }
        public string? PaymentStatus { get; private set; }
        public Order? Order { get; set; }
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();

        private Invoice() { }
        public Invoice(int orderId, decimal totalAmount)
        {
            OrderID = orderId;
            InvoiceDate = DateTime.UtcNow;
            TotalAmount = totalAmount;
            PaymentStatus = "Unpaid";
        }
        public void UpdatePaymentStatus(string paid)
        {
            PaymentStatus = paid;
        }
    }
}
