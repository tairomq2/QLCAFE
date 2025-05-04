using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction? _transaction;


        public UnitOfWork(ApplicationDbContext context, IProductRepository productRepository, IEmployeeRepository employeeRepository, IUserRepository userRepository, IUserRoleRepository userRoleRepository, IOrderRepository orderRepository, IInvoiceRepository invoiceRepository, IPaymentRepository paymentRepository)
        {
            _context = context;
            Products = productRepository;
            Employees = employeeRepository;
            Users = userRepository;
            UserRoles = userRoleRepository;
            Orders = orderRepository;
            Invoices = invoiceRepository;
            Payments = paymentRepository;
        }
        public IProductRepository Products { get; }
        public IEmployeeRepository Employees { get; }
        public IUserRepository Users { get; }
        public IUserRoleRepository UserRoles { get; }
        public IOrderRepository Orders { get; }
        public IInvoiceRepository Invoices { get; }
        public IPaymentRepository Payments { get; }
        //bắt đầu transaction
        public async Task BeginTranSacationAsync()
        {
            _transaction ??= await _context.Database.BeginTransactionAsync();
        }
        //Comit Transaction nếu không có lỗi
        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _context.SaveChangesAsync();
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public void DisposeAsync()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }

        //Rollback Transaction nếu có lỗi

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
        //Lưu thay đổi vào database
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

    }
}
