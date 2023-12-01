using OnlineShopProject.Models;

namespace OnlineShopProject
{
    public interface IProductsRepository
    {
        public ProductModel GetProductById(int id);
        public List<ProductModel> GetAllProducts();
        public void DeleteProduct(int id);
        public void EditProduct(ProductModel model);
        public void AddProduct(ProductModel model);
        public List<ProductModel> SearchProducts(string name);
    }
}
