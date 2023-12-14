
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
        public List<Product> GetByUserId(string userId)
        {
            return _context.Favorites.Include(x => x.Product)
                .Where(x => x.UserId == userId)
                .Select(x => x.Product)
                .ToList();
        }
        public Favorite GetById(Guid productId, string userId)
        {
            return _context.Favorites.Include(x => x.Product)
                .FirstOrDefault(x => x.UserId == userId && x.Product.Id == productId);
        }
        public void Add(Product product, string userId)
        {
            var check = GetById(product.Id, userId);
            if (check == null)
            {
                var favorite = new Favorite
                {
                    Product = product,
                    UserId = userId
                };
                _context.Favorites.Add(favorite);
            }
            var model = GetByUserId(userId);
            _context.SaveChanges();
        }
        public void Remove(Guid productId, string userId)
        {
            var model = GetById(productId, userId);
            if (model != null)
            {
                _context.Remove(model);
            }
            _context.SaveChanges();
        }
    }
}
