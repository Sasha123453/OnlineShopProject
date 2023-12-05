using OnlineShopProject.Models;

namespace OnlineShopProject
{
    public interface IOrdersRepository
    {
        public List<Order> GetOrdersByUserId(string userId);
        public void AddToOrders(Cart cart, AddressModel orderInfo);
    }
}
