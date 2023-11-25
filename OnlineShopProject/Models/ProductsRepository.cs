namespace OnlineShopProject.Models
{
    public class ProductsRepository : IProductsRepository
    {
        private List<ProductModel> products = new List<ProductModel> { new ProductModel(1, "Iphone 15", "Описание", "Overprice", 100000,8,"A17",8,3.4), new ProductModel(2, "Xiaomi 14", "Описание", "Overprice", 100000,16,"8 gen 3",8,3.7), new ProductModel(3, "Meizu 15", "Описание", "Overprice", 100000,16,"Snapdragon 855",8,2.5) };
        public ProductModel GetProductById(int id)
        {
            return products.FirstOrDefault(x => x.Id == id);
        }
        public List<ProductModel> GetAllProducts()
        {
            return products;
        }
    }
}
