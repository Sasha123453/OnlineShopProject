using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using System;

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
            return _context.Carts.Include(x => x.Items).ThenInclude(x => x.Product).Where(x => x.UserId == userId).FirstOrDefault();
        }
        public void AddToCart(string userId, Product model)
        {
            Cart cart = GetCartByUserId(userId);
            if (cart == null)
            {
                Cart newCart = new Cart
                {
                    UserId = userId
                };
                CartItem item = new CartItem
                {
                    Product = model,
                    Amount = 1
                };
                newCart.Items = new List<CartItem> { item };
                _context.Add(newCart);
            }
            else
            {
                CartItem cartItem = cart.Items.FirstOrDefault(x => x.ProductId == model.Id);
                if (cartItem == null)
                {
                    CartItem item = new CartItem
                    {
                        Product = model,
                        Amount = 1
                    };
                    cart.Items.Add(item);
                }
                else
                {
                    cartItem.Amount++;
                }
            }
            _context.SaveChanges();
        }
        public void DeleteCartItems(List<CartItem> items, string userId)
        {
            var cart = GetCartByUserId(userId);
            foreach (var item in items)
            {
                cart.Items.Remove(item);
            }
            _context.SaveChanges();
        }
        public void DecreaseAmount(Guid id, string userId)
        {
            Cart cart = GetCartByUserId(userId);
            var item = cart.Items.FirstOrDefault(x => x.ProductId == id);
            if (item != null)
            {
                if (item.Amount == 1) _context.Remove(item);
                else item.Amount--;
            }
            _context.SaveChanges();
        }
        public List<CartItem> GetCartItemsByIds(List<Guid> ids, string userId)
        {
            var cart = GetCartByUserId(userId);
            return cart.Items.Where(x => ids.Contains(x.ProductId)).ToList();
        }
    }
}
