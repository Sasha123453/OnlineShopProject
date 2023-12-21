using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Interfaces
{
    public interface IComparsionRepository
    {
        public Task<List<Product>> GetByUserIdAsync(string userId);
        public Task<Comparsion> GetByIdAsync(Guid productId, string userId);
        public Task AddAsync(Product product, string userId);
        public Task RemoveAsync(Guid productId, string userId);
    }
}
