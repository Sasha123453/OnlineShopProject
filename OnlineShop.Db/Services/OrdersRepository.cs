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
        public void Add(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }
        public void Edit(Guid orderId, OrderStatus status)
        {
            GetById(orderId).Status = status;
            _context.SaveChanges();
        }
        public List<Order> GetAll()
        {
            return _context.Orders.Include(x => x.Info).Include(x => x.CartItems).ThenInclude(x => x.Product).ToList(); 
        }
        public List<Order> GetByUserId(string userId)
        {
            return _context.Orders.Where(x => x.UserId == userId).ToList();
        }
        public Order GetById(Guid id)
        {
            return _context.Orders.FirstOrDefault(x => x.Id == id);
        }
    }
}
