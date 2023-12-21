

using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Interfaces
{
    public interface ICartsRepository
    {
        public Task<Cart> GetByUserIdAsync(string userId);
        public Task AddAsync(string userId, Product model);
        public Task DecreaseAmountAsync(Guid id, string userId);
        public Task<List<CartItem>> GetByIdsAsync(List<Guid> ids, string userId);
        public Task RemoveItemsAsync(List<CartItem> items, string userId);
        public Task<int> GetCountByUserIdAsync(string userId);
        public Task AddCartAsync(List<CartItem> newCart, string userId);
    }
}
