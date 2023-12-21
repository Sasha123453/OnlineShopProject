using OnlineShop.Db.Models;

namespace OnlineShop.Db.Interfaces
{
    public interface IProductsRepository
    {
        public Task<Product> GetByIdAsync(Guid id);
        public Task<List<Product>> GetAllAsync();
        public Task RemoveAsync(Guid id);
        public Task EditAsync(Product model);
        public Task AddAsync(Product model);
        public Task<List<Product>> SearchAsync(string name);
    }
}
