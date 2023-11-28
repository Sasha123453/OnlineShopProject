using OnlineShopProject.Models;

namespace OnlineShopProject.Interfaces
{
    public interface ICartsRepository
    {
        public Cart GetCartByUserId(string userId);
        public void AddToCart(string userId, ProductModel model);
        public void ChangeAmount(int change, int id, string userId);
        public void RemoveCart(string userId);
    }
}
