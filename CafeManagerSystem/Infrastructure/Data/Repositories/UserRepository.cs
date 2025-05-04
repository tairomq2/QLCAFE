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
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> AddAsync(User entity)
        {
            await _context.Users.AddAsync(entity);
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Users.FindAsync(id);
            if(product != null)
            {
                _context.Users.Remove(product);
            }
        }

        public async Task<bool> ExistUserAsync(int employeeId)
        {
            return await _context.Users.AnyAsync(u => u.EmployeeID == employeeId);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
             return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<string?> GetRoleNameByIdAsync(int useId)
        {
           return await _context.UserRoles.Where(ur => ur.RoleID == useId).Select(ur => ur.RoleName).FirstOrDefaultAsync();
        }


        public async Task<User?> GetUserByUsernameAsync(string username)
        {
           return await _context.Users.Where(u => u.Username == username).FirstOrDefaultAsync();

        }

        public  Task UpdateAsync(User entity)
        {
            throw new NotImplementedException();

        }
    }
}
