
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Services
{
    public class ComparsionRepository : IComparsionRepository
    {
        private readonly ApplicationContext _context;
        public ComparsionRepository(ApplicationContext context)
        {
            _context = context;
        }
        public List<Product> GetUserComparsions(string userId)
        {
            return _context.Comparsions.Include(x => x.Product)
                .Where(x => x.UserId == userId)
                .Select(x => x.Product)
                .ToList();
        }
        public Comparsion GetComparsion(Guid productId, string userId)
        {
            return _context.Comparsions.Include(x => x.Product)
                .FirstOrDefault(x => x.Product.Id == productId && x.UserId == userId)!;
        }
        public void AddToComparsion(Product product, string userId)
        {
            var check = GetComparsion(product.Id, userId);
            if (check == null)
            {
                var comparsion = new Comparsion
                {
                    UserId = userId,
                    Product = product,
                };
                _context.Add(comparsion);
            }
            _context.SaveChanges();
        }
        public void Delete(Guid productId, string userId)
        {
            var model = GetComparsion(productId, userId);
            if (model != null)
            {
                _context.Comparsions.Remove(model);
            }
            _context.SaveChanges();
        }
    }
}
