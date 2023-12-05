using OnlineShopProject.Models;

namespace OnlineShopProject
{
    public interface IComparsionRepository
    {
        public UserWithProductsModel GetAllUserComparsions(string userId);
        public void AddToComparsion(ProductViewModel product, string userId);
        public void Delete(ProductViewModel product, string userId);
    }
}
