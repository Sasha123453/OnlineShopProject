

using OnlineShop.Db.Models;

namespace OnlineShop.Db.Interfaces
{
    public interface ICartsRepository
    {
        public Cart GetCartByUserId(string userId);
        public void AddToCart(string userId, Product model);
        public void DecreaseAmount(Guid id, string userId);
    }
}
