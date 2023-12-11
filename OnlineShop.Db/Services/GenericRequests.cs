using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;


namespace OnlineShop.Db.Services
{
    public class GenericRequests : IGenericRequests
    {
        private readonly ApplicationContext _context;
        public GenericRequests(ApplicationContext context)
        {
            _context = context;
        }
        public List<ProductPage> GetProductPage(string userId, int page)
        {
            var x = (from product in _context.Products
                     join cartItem in _context.CartItems on product.Id equals cartItem.ProductId into temp
                     join favorite in _context.Favorites on product.Id equals favorite.ProductId into temp1
                     from cartItem in temp.DefaultIfEmpty()
                     from favorite in temp1.DefaultIfEmpty()
                     where product.Id == cartItem.Id && cartItem.Id == favorite.Id
                     select new ProductPage
                     {
                         Product = product,
                         IsInCart = cartItem != null,
                         IsInFavorite = favorite != null
                     }).Skip(5).Take(10).ToList();
            return x;

        }

    }
}
