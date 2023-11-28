using OnlineShopProject.Interfaces;
using OnlineShopProject.Models;

namespace OnlineShopProject.InMemoryModels
{
    public class OrdersRepository : IOrdersRepository
    {
        public List<Order> Orders = new List<Order>();
        public Order GetOrderByUserId(string userId)
        {
            return Orders.FirstOrDefault(x => x.Cart.UserId == userId);
        }
        public void AddToOrders(Cart cart, OrderInfoModel orderInfo)
        {
            Order order = new Order
            {
                Cart = cart,
                Id = Guid.NewGuid(),
                Info = orderInfo
            };
            Orders.Add(order);
        }
        public void RemoveOrder(string userId)
        {
            var order = GetOrderByUserId(userId);
            if (order != null) order.Cart.Items.Clear();
        }
    }
}
