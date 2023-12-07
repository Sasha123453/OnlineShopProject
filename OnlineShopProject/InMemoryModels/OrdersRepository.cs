using OnlineShop.Db.Interfaces;
using OnlineShopProject.Interfaces;
using OnlineShopProject.Models;

namespace OnlineShopProject.InMemoryModels
{
    public class OrdersRepository : IOrdersRepository
    {
        public List<Order> Orders = new List<Order>();
        public List<Order> GetOrdersByUserId(string userId)
        {
            return Orders.Where(x => x.Cart.UserId == userId).ToList();
        }
        public void AddToOrders(CartViewModel cart, DeliveryInfoItemViewModel orderInfo)
        {
            Order order = new Order
            {
                Cart = cart,
                Id = Guid.NewGuid(),
                Info = orderInfo
            };
            Orders.Add(order);
        }
    }
}
