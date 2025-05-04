using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetUserByUsernameAsync(string username);
        Task<string?> GetRoleNameByIdAsync(int userId);
        Task<bool> ExistUserAsync(int employeeId);
    }
}
