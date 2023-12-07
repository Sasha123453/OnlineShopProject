using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Services
{
    public class CartsRepository : ICartsRepository
    {
        private readonly ApplicationContext _context;
        public CartsRepository(ApplicationContext context)
        {
            _context = context;
        }
        public Cart GetCartByUserId(string userId)
        {
            return _context.Carts.Include(x => x.Items).ThenInclude(x => x.Product).FirstOrDefault(x => x.UserId == userId);
        }
        public void AddToCart(string userId, Product model)
        {
            Cart cart = GetCartByUserId(userId);
            if (cart == null)
            {

                Cart newCart = new Cart
                {
                    UserId = userId,
                };
                newCart.Items = new List<CartItem> { new CartItem { Product = model, Amount = 1, Cart = newCart } };
                _context.Carts.Add(newCart);
            }
            else
            {
                var item = cart.Items.FirstOrDefault(x => x.Product.Id == model.Id);
                if (item == null)
                {
                    CartItem newitem = new CartItem{ Product = model, Amount = 1, Cart = cart };
                    cart.Items.Add(newitem);
                }
                else
                {
                    item.Amount++;
                }
            }
            _context.SaveChanges();
        }
        public void DecreaseAmount(Guid id, string userId)
        {
            var cart = GetCartByUserId(userId);
            var toChange = cart.Items.FirstOrDefault(x => x.Product.Id == id);
            if (toChange.Amount == 1) cart.Items.Remove(toChange);
            else toChange.Amount--;
            _context.SaveChanges();
        }
    }
}
