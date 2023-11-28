using OnlineShopProject.Models;

namespace OnlineShopProject
{
    public interface IProductsRepository
    {
        public ProductModel GetProductById(int id);
        public List<ProductModel> GetAllProducts();
    }
}
