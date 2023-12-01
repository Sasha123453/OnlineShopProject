using Microsoft.AspNetCore.Mvc.ModelBinding;
using OnlineShopProject.Interfaces;
using OnlineShopProject.Models;

namespace OnlineShopProject.InMemoryModels
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly IConstances _constances;
        public ProductsRepository(IConstances constances)
        {
            _constances = constances;
        }
        private List<ProductModel> products = new List<ProductModel> { new ProductModel(1, "Iphone 15", "Описание", "Overprice", 100000, 8, "A17", 8, 3.4), new ProductModel(2, "Xiaomi 14", "Описание", "Overprice", 100000, 16, "8 gen 3", 8, 3.7), new ProductModel(3, "Meizu 15", "Описание", "Overprice", 100000, 16, "Snapdragon 855", 8, 2.5) };
        public ProductModel GetProductById(int id)
        {
            return products.FirstOrDefault(x => x.Id == id);
        }
        public List<ProductModel> GetAllProducts()
        {
            return products;
        }
        public List<ProductModel> SearchProducts(string name)
        {
            return products.Where(x => x.Name == name).ToList();
        }
        public void DeleteProduct(int id)
        {
            products.Remove(products.FirstOrDefault(x => x.Id == id));
        }

        public void EditProduct(ProductModel model)
        {
            var toEdit = products.FirstOrDefault(x => x.Id == model.Id);
            if (toEdit != null)
            {
                toEdit.Name = model.Name;
                toEdit.Description = model.Description;
                toEdit.Price = model.Price;
                toEdit.Category = model.Category;
                toEdit.Ram = model.Ram;
                toEdit.Cpu = model.Cpu;
                toEdit.CoresAmount = model.CoresAmount;
                toEdit.MaxFrequency = model.MaxFrequency;
            }
        }
        public void AddProduct(ProductModel model)
        {
            model.Id = _constances.ProductId;
            products.Add(model);
        }
    }
}
