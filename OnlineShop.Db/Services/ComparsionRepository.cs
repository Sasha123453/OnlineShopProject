
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Services
{
    public class ComparsionRepository : IComparsionRepository
    {
        private readonly ApplicationContext _context;
        public ComparsionRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<List<Product>> GetByUserIdAsync(string userId)
        {
            return await _context.Comparsions.Include(x => x.Product)
                .Where(x => x.UserId == userId)
                .Select(x => x.Product)
                .ToListAsync();
        }
        public async Task<Comparsion> GetByIdAsync(Guid productId, string userId)
        {
            return await _context.Comparsions.Include(x => x.Product)
                .FirstOrDefaultAsync(x => x.Product.Id == productId && x.UserId == userId)!;
        }
        public async Task AddAsync(Product product, string userId)
        {
            var check = await GetByIdAsync(product.Id, userId);
            if (check == null)
            {
                var comparsion = new Comparsion
                {
                    UserId = userId,
                    Product = product,
                };
                _context.Add(comparsion);
            }
            await _context.SaveChangesAsync();
        }
        public async Task RemoveAsync(Guid productId, string userId)
        {
            await _context.Comparsions.Where(x => x.ProductId == productId && x.UserId == userId).ExecuteDeleteAsync();
        }
    }
}
