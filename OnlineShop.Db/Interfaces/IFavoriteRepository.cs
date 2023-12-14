using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Interfaces
{
    public interface IFavoriteRepository
    {
        public List<Product> GetByUserId(string userId);
        public Favorite GetById(Guid productId, string userId);
        public void Add(Product product, string userId);
        public void Remove(Guid productId, string userId);
    }
}
