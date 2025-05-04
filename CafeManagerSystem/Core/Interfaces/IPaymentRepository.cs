using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        Task<decimal> GetTotalPaidAmountAsync(int invoiceId);
    }
}
