using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopProject.Models;

namespace OnlineShop.Db.Services
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly ApplicationContext _context;
        public OrdersRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }
        public async Task EditAsync(Guid orderId, OrderStatus status)
        {
            await _context.Orders.Where(x => x.Id == orderId).ExecuteUpdateAsync(x => x.SetProperty(x => x.Status, status));
        }
        public async Task<List<Order>> GetAllAsync()
        {
            return await _context.Orders.Include(x => x.Info).Include(x => x.CartItems).ThenInclude(x => x.Product).ToListAsync(); 
        }
        public async Task<List<Order>> GetByUserIdAsync(string userId)
        {
            return await _context.Orders.Where(x => x.UserId == userId).ToListAsync();
        }
        public async Task<Order> GetByIdAsync(Guid id)
        {
            return await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
