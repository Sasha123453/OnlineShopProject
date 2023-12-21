

using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;
using OnlineShopProject.Models;

namespace OnlineShop.Db.Interfaces
{
    public interface IOrdersRepository
    {
        public Task AddAsync(Order order);
        public Task EditAsync(Guid orderId, OrderStatus status);
        public Task<List<Order>> GetAllAsync();
        public Task<List<Order>> GetByUserIdAsync(string userId);
        public Task<Order> GetByIdAsync(Guid id);
    }
}
