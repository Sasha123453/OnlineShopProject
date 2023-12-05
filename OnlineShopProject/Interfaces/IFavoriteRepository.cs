using OnlineShopProject.Models;

namespace OnlineShopProject
{
    public interface IFavoriteRepository
    {
        public UserWithProductsModel GetAllUserFavorites(string userId);
        public void AddToFavorites(ProductViewModel product, string userId);
        public void Delete(ProductViewModel product, string userId);
    }
}
