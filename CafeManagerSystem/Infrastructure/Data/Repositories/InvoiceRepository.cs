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
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly ApplicationDbContext _context;
        public InvoiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Invoice> AddAsync(Invoice entity)
        {
            await _context.AddAsync(entity);
            return entity;
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Invoice>> GetAllAsync()
        {
            return await _context.Invoices.ToListAsync();
        }

        public async Task<Invoice?> GetByIdAsync(int id)
        {
            return await _context.Invoices.FirstOrDefaultAsync(x => x.OrderID == id);
        }
        public async Task<Invoice?> GetInvoiceByIdAsync(int id)
        {
            return await _context.Invoices.FirstOrDefaultAsync(x => x.InvoiceID == id);
        }

        public async Task UpdateAsync(Invoice entity)
        {
            _context.Invoices.Update(entity);
            await _context.SaveChangesAsync();
        }
        public  IQueryable<Invoice> GetInvoiceBy()
        {
            return  _context.Invoices.AsQueryable();
        }
    }
}
