using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Product
    {
        [Key]
        public int ProductID { get;private set; }
        public string ProductName { get; private set; } = string.Empty;
        public string? Description { get; private set; }
        public decimal UnitPrice { get; private set; }
        public int StockQuantity { get; private set; }
        public string Category { get; private set; } = string.Empty;
        public bool IsActive { get; private set; } = true;
        public ICollection<ProductPrice> ProductPrices { get; set; } = new List<ProductPrice>();
        public ICollection<InventoryLog> InventoryLogs { get; set; } = new List<InventoryLog>();
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
        public Inventory? Inventory { get; set; }
        private Product() { }
        public Product(string name, string? description, decimal price, int stock, string category)
        {
            ProductName = name;
            Description = description;
            UnitPrice = price;
            StockQuantity = stock;
            Category = category;
            IsActive = true;
        }
        public void UpdateProduct(string name, string? description, decimal price, int stock, string category)
        {
            ProductName = name;
            Description = description;
            UnitPrice = price;
            StockQuantity = stock;
            Category = category;
        }
    }
}
