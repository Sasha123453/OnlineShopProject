using OnlineShopProject.Models;

namespace OnlineShopProject.Interfaces
{
    public interface ICartsRepository
    {
        public Cart GetCartByUserId(string userId);
        public void AddToCart(string userId, ProductViewModel model);
        public void ChangeAmount(int change, Guid id, string userId);
        public void RemoveCart(string userId);
    }
}
