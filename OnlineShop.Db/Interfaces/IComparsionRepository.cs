using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Interfaces
{
    public interface IComparsionRepository
    {
        public List<Product> GetByUserId(string userId);
        public Comparsion GetById(Guid productId, string userId);
        public void Add(Product product, string userId);
        public void Remove(Guid productId, string userId);
    }
}
