using OnlineShopProject.Models;

namespace OnlineShopProject
{
    public interface IOrdersRepository
    {
        public Order GetOrderByUserId(string userId);
        public void AddToOrders(Cart cart, OrderInfoModel orderInfo);
        public void RemoveOrder(string userId);
    }
}
