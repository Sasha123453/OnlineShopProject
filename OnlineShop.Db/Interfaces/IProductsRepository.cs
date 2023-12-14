using OnlineShop.Db.Models;

namespace OnlineShop.Db.Interfaces
{
    public interface IProductsRepository
    {
        public Product GetById(Guid id);
        public List<Product> GetAll();
        public void Remove(Guid id);
        public void Edit(Product model);
        public void Add(Product model);
        public List<Product> Search(string name);
    }
}
