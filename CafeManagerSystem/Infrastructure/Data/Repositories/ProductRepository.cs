using Core.Entities;
using Core.Interfaces;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Product> AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            return product;
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<Product?> GetByNameAsync(string name)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.ProductName == name);
        }

        public  IQueryable<Product> GetProductBy()
        {
            return  _context.Products.AsQueryable();
        }

        public async Task UpdateAsync(Product entity)
        {
            var existingProduct = await _context.Products.FindAsync(entity.ProductID);
            if (existingProduct != null)
            {
                existingProduct.UpdateProduct(entity.ProductName, entity.Description, entity.UnitPrice, entity.StockQuantity, entity.Category);
            }
            await _context.SaveChangesAsync();
        }
    }
}
