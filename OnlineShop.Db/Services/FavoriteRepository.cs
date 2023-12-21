
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Services
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly ApplicationContext _context;
        public FavoriteRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<List<Product>> GetByUserIdAsync(string userId)
        {
            return await _context.Favorites.Include(x => x.Product)
                .Where(x => x.UserId == userId)
                .Select(x => x.Product)
                .ToListAsync();
        }
        public async Task<Favorite> GetByIdAsync(Guid productId, string userId)
        {
            return await _context.Favorites.Include(x => x.Product)
                .FirstOrDefaultAsync(x => x.UserId == userId && x.Product.Id == productId);
        }
        public async Task AddAsync(Product product, string userId)
        {
            var check = await GetByIdAsync(product.Id, userId);
            if (check == null)
            {
                var favorite = new Favorite
                {
                    Product = product,
                    UserId = userId
                };
                _context.Favorites.Add(favorite);
            }
            var model = await GetByUserIdAsync(userId);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveAsync(Guid productId, string userId)
        {
            await _context.Favorites.Where(x => x.ProductId == productId && x.UserId == userId).ExecuteDeleteAsync();
        }
    }
}
