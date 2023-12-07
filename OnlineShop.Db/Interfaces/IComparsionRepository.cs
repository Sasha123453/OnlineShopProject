using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Interfaces
{
    public interface IComparsionRepository
    {
        public List<Product> GetUserComparsions(string userId);
        public Comparsion GetComparsion(Guid productId, string userId);
        public void AddToComparsion(Product product, string userId);
        public void Delete(Guid productId, string userId);
    }
}
