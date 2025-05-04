using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository Products { get; }
        IEmployeeRepository Employees { get; }
        IUserRepository Users { get; }
        IUserRoleRepository UserRoles { get; }
        IOrderRepository Orders { get; }
        IInvoiceRepository Invoices { get; }
        IPaymentRepository Payments { get; }
        Task<int> SaveChangesAsync();
        Task BeginTranSacationAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
