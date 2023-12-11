

using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;
using OnlineShopProject.Models;

namespace OnlineShop.Db.Interfaces
{
    public interface IOrdersRepository
    {
        public void AddToOrders(Order order);
        public List<Order> GetAll();
        public void EditOrder(Guid orderId, OrderStatus status);
        public List<Order> GetOrdersByUserId(string userId);
        public Order GetOrderById(Guid id);
    }
}
