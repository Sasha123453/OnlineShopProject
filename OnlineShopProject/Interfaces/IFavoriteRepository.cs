using OnlineShopProject.Models;

namespace OnlineShopProject
{
    public interface IFavoriteRepository
    {
        public UserWithProductsModel GetAllUserFavorites(string userId);
        public void AddToFavorites(ProductModel product, string userId);
        public void Delete(ProductModel product, string userId);
    }
}
