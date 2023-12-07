using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Interfaces
{
    public interface IFavoriteRepository
    {
        public List<Product> GetUserFavorites(string userId);
        public Favorite GetFavorite(Guid productId, string userId);
        public void AddToFavorites(Product product, string userId);
        public void Delete(Guid productId, string userId);
    }
}
