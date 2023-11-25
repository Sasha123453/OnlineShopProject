using OnlineShopProject.Models;

namespace OnlineShopProject
{
    public interface IComparsionRepository
    {
        public ComparsionModel GetAllUserComparsions(string userId);
        public void AddToComparsion(ProductModel product, string userId);
    }
}
