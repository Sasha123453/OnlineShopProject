

using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;
using OnlineShopProject.Models;

namespace OnlineShop.Db.Interfaces
{
    public interface IOrdersRepository
    {
        public void Add(Order order);
        public void Edit(Guid orderId, OrderStatus status);
        public List<Order> GetAll();
        public List<Order> GetByUserId(string userId);
        public Order GetById(Guid id);
    }
}
