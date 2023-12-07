
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Services
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly ApplicationContext _context;
        public FavoriteRepository(ApplicationContext context)
        {
            _context = context;
        }
        public List<Product> GetUserFavorites(string userId)
        {
            return _context.Favorites.Include(x => x.Product)
                .Where(x => x.UserId == userId)
                .Select(x => x.Product)
                .ToList();
        }
        public Favorite GetFavorite(Guid productId, string userId)
        {
            return _context.Favorites.Include(x => x.Product)
                .FirstOrDefault(x => x.UserId == userId && x.Product.Id == productId);
        }
        public void AddToFavorites(Product product, string userId)
        {
            var check = GetFavorite(product.Id, userId);
            if (check == null)
            {
                var favorite = new Favorite
                {
                    Product = product,
                    UserId = userId
                };
                _context.Favorites.Add(favorite);
            }
            var model = GetUserFavorites(userId);
            _context.SaveChanges();
        }
        public void Delete(Guid productId, string userId)
        {
            var model = GetFavorite(productId, userId);
            if (model != null)
            {
                _context.Remove(model);
            }
            _context.SaveChanges();
        }
    }
}
