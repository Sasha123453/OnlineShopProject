using OnlineShopProject.Models;

namespace OnlineShop.Db.Models
{
    public class Order
    {
        public List<CartItem> CartItems { get; set; }
        public string UserId { get; set; }
        public OrderStatus Status { get; set; }
        public Guid Id { get; set; }
        public DeliveryInfoItem Info { get; set; }
    }
}
