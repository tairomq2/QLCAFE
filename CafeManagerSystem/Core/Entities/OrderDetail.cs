using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; } = 0;

        // Navigation properties
        public Order? Order { get; set; }
        public Product? Product { get; set; } // Thêm dòng này

        public OrderDetail(int orderID, int productID, int quantity, decimal unitPrice, decimal discount = 0)
        {
            if (quantity <= 0) throw new ArgumentException("Số lượng phải lớn hơn 0");
            if (unitPrice < 0) throw new ArgumentException("Số tiền phải số dương");
            if (discount < 0) throw new ArgumentException("Phần trăm giảm giá không thể âm");
            OrderID = orderID;
            ProductID = productID;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Discount = discount;
        }
        public void UpdateQuantity(int quantity)
        {
            if (quantity <= 0) throw new ArgumentException("Số lượng phải lớn hơn 0");
            Quantity = quantity;
        }
        public void UpdatePrice(decimal price)
        {
            if (price < 0) throw new ArgumentException("Số tiền phải số dương");
            UnitPrice = price;
        }
        public void UpdateDiscount(decimal discount)
        {
            if (discount < 0) throw new ArgumentException("Phần trăm giảm giá không thể âm");
            Discount = discount;
        }
        public decimal CalculateTotal()
        {
            return (Quantity * UnitPrice) - ((Discount * UnitPrice)/100);
        }
    }
}
