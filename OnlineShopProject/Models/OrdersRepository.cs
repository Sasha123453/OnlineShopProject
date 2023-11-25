namespace OnlineShopProject.Models
{
    public class OrdersRepository : IOrdersRepository
    {
        public List<Order> Orders = new List<Order>();
        public Order GetOrderById(string userId)
        {
            return Orders.FirstOrDefault(x => x.Cart.UserId == userId);
        }
        public void AddOrder(Order order) { }
    }
}
