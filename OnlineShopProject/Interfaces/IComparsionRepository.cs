using OnlineShopProject.Models;

namespace OnlineShopProject
{
    public interface IComparsionRepository
    {
        public UserWithProductsModel GetAllUserComparsions(string userId);
        public void AddToComparsion(ProductModel product, string userId);
        public void Delete(ProductModel product, string userId);
    }
}
