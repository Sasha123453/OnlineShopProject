using OnlineShop.Db.Models;

namespace OnlineShopProject.Models
{
    public class OrderViewModel
    {
        public List<CartItemViewModel> CartItems { get; set; }
        public string UserId { get; set; }
        public Guid Id { get; set; }
        public OrderStatus Status { get; set; }
        public DeliveryInfoItemViewModel Info { get; set; }
        public decimal Cost { get 
            {
                return CartItems.Sum(x => x.Cost);
            } 
        }
    }
}
