using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        public int TableID { get; set; }
        public int? EmployeeID { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Pending";

        public Invoice? Invoice { get; set; }
        public CoffeeTable? CoffeeTable { get; set; } 
        public Employee? Employee { get; set; } 
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        public Order(int tableID, int? employeeID, decimal totalAmount, string status)
        {
            TableID = tableID;
            EmployeeID = employeeID;
            OrderDate = DateTime.UtcNow;
            TotalAmount = totalAmount;
            Status = status;
        }
        // Hàm cập nhật EmployeeID khi nhân viên xác nhận Order
        public void ConfirmOrder(int employeeId)
        {
            EmployeeID = employeeId;
            Status = "Confirmed";
        }
        public void AddOrderDetail(OrderDetail orderDetail)
        {
            if (OrderDetails == null)
            {
                throw new ArgumentNullException(nameof(OrderDetails));
            }
            OrderDetails.Add(orderDetail);
        }
        public void ChangeStatus(string status)
        {
            Status = status;
        }
        public void ChangeCompletedStatus()
        {
            Status = "Completed";
        }
        public void UpdateEmployee(int employeeId)
        {
            EmployeeID = employeeId;
        }
    }
}
