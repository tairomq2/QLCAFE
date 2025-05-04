using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;
        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Payment> AddAsync(Payment entity)
        {
            await _context.AddAsync(entity);
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
            }
        }

        public async Task<IEnumerable<Payment>> GetAllAsync()
        {
            return await _context.Payments.ToListAsync();
        }

        public async Task<Payment?> GetByIdAsync(int id)
        {
            return await _context.Payments.FindAsync(id);
        }

        public async Task<decimal> GetTotalPaidAmountAsync(int invoiceId)
        {
            return await _context.Payments.Where(p => p.InvoiceID == invoiceId).SumAsync(p => p.Amount);
        }

        public Task UpdateAsync(Payment entity)
        {
            throw new NotImplementedException();
        }
    }
}
