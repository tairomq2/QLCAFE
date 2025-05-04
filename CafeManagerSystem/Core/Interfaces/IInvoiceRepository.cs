using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IInvoiceRepository : IRepository<Invoice>
    {
        IQueryable<Invoice> GetInvoiceBy();
        Task<Invoice?> GetInvoiceByIdAsync(int id);
    }
}
