using OnlineShop.Db.Models;

namespace OnlineShop.Db.Interfaces
{
    public interface IProductsRepository
    {
        public Product GetProductById(Guid id);
        public List<Product> GetAllProducts();
        public void DeleteProduct(Guid id);
        public void EditProduct(Product model);
        public void AddProduct(Product model);
        public List<Product> SearchProducts(string name);
    }
}
