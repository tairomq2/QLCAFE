using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        #region Dbset
        public DbSet<Employee> Employees { get; set; }
        public DbSet<CoffeeTable> CoffeeTables { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<InventoryLog> InventoryLogs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        public DbSet<ProductPrice> ProductPrices { get; set; }

        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình quan hệ bảng Employees
            modelBuilder.Entity<User>()
                .HasOne(u => u.Employee)
                .WithOne()
                .HasForeignKey<User>(u => u.EmployeeID);

            // Cấu hình quan hệ bảng Orders
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Employee)
                .WithMany(e => e.Orders)
                .HasForeignKey(o => o.EmployeeID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.CoffeeTable)
                .WithMany(t => t.Orders)
                .HasForeignKey(o => o.TableID)
                .OnDelete(DeleteBehavior.Restrict);

            // Cấu hình quan hệ bảng OrderDetails
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.ProductID)
                .OnDelete(DeleteBehavior.Restrict);

            // Cấu hình quan hệ bảng Invoices
            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Order)
                .WithOne(o => o.Invoice)
                .HasForeignKey<Invoice>(i => i.OrderID)
                .OnDelete(DeleteBehavior.Cascade);

            // Cấu hình quan hệ bảng Payments
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Invoice)
                .WithMany(i => i.Payments)
                .HasForeignKey(p => p.InvoiceID)
                .OnDelete(DeleteBehavior.Cascade);

            // Cấu hình quan hệ bảng Inventory
            modelBuilder.Entity<Inventory>()
                .HasOne(i => i.Product)
                .WithOne(p => p.Inventory)
                .HasForeignKey<Inventory>(i => i.ProductID)
                .OnDelete(DeleteBehavior.Cascade);

            // Cấu hình quan hệ bảng InventoryLogs
            modelBuilder.Entity<InventoryLog>()
                .HasOne(il => il.Product)
                .WithMany(p => p.InventoryLogs)
                .HasForeignKey(il => il.ProductID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<InventoryLog>()
                .HasOne(il => il.Employee)
                .WithMany(e => e.InventoryLogs)
                .HasForeignKey(il => il.EmployeeID)
                .OnDelete(DeleteBehavior.Restrict);

            // Cấu hình quan hệ bảng Salaries
            modelBuilder.Entity<Salary>()
                .HasOne(s => s.Employee)
                .WithMany(e => e.Salaries)
                .HasForeignKey(s => s.EmployeeID)
                .OnDelete(DeleteBehavior.Cascade);

            // Cấu hình quan hệ bảng ProductPrices
            modelBuilder.Entity<ProductPrice>()
                .HasOne(pp => pp.Product)
                .WithMany(p => p.ProductPrices)
                .HasForeignKey(pp => pp.ProductID)
                .OnDelete(DeleteBehavior.Cascade);


        }
}
}
