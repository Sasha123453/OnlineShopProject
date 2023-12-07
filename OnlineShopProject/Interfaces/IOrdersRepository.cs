using OnlineShopProject.Models;

namespace OnlineShop.Db.Interfaces
{
    public interface IOrdersRepository
    {
        public List<Order> GetOrdersByUserId(string userId);
        public void AddToOrders(CartViewModel cart, DeliveryInfoItemViewModel orderInfo);
    }
}
