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
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserRole> AddAsync(UserRole entity)
        {
            await _context.UserRoles.AddAsync(entity);
            return entity;
        }

        public  Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserRole>> GetAllAsync()
        {
            return await _context.UserRoles.ToListAsync();
        }

        public async Task<UserRole?> GetByIdAsync(int id)
        {
            return await _context.UserRoles.FindAsync(id);
        }

        public async Task<UserRole?> GetRoleNameByPosition(string position)
        {
            return await _context.UserRoles.Where(r => r.RoleName == position).FirstOrDefaultAsync();
        }

        public Task UpdateAsync(UserRole entity)
        {
            throw new NotImplementedException();
        }
    }
}
