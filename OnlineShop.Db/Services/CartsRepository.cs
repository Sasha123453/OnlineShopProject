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
        public async Task<Cart> GetByUserIdAsync(string userId)
        {
            return await _context.Carts.Include(x => x.Items).ThenInclude(x => x.Product).Where(x => x.UserId == userId).FirstOrDefaultAsync();
        }
        public async Task AddAsync(string userId, Product model)
        {
            var cart = await GetByUserIdAsync(userId);
            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    Items = new List<CartItem>()
                };
                var item = new CartItem
                {
                    Product = model,
                    Amount = 1
                };
                cart.Items.Add(item);
                _context.Add(cart);
            }
            else
            {
                var item = cart.Items.FirstOrDefault(x => x.ProductId == model.Id);
                if (item == null)
                {
                    item = new CartItem
                    {
                        Product = model,
                        Amount = 1
                    };
                    cart.Items.Add(item);
                }
                else item.Amount++;
            }
            await _context.SaveChangesAsync();
        }
        public async Task<int> GetCountByUserIdAsync(string userId)
        {
            return await _context.Carts.Where(User => User.UserId == userId).CountAsync();
        }
        public async Task RemoveItemsAsync(List<CartItem> items, string userId)
        {
            var cart = await GetByUserIdAsync(userId);
            var intersect = cart.Items.Intersect(items);
            await _context.CartItems.Where(x => intersect.Contains(x)).ExecuteDeleteAsync();
        }
        public async Task DecreaseAmountAsync(Guid id, string userId)
        {
            Cart cart = await GetByUserIdAsync(userId);
            var item = cart.Items.FirstOrDefault(x => x.ProductId == id);
            if (item != null)
            {
                if (item.Amount == 1) _context.Remove(item);
                else item.Amount--;
            }
            await _context.SaveChangesAsync();
        }
        public async Task<List<CartItem>> GetByIdsAsync(List<Guid> ids, string userId)
        {
            var cart = await GetByUserIdAsync(userId);
            return cart.Items.Where(x => ids.Contains(x.ProductId)).ToList();
        }
        public async Task<Cart> InitializeCart(string userId)
        {
            Cart cart = new Cart
            {
                UserId = userId,
                Items = new List<CartItem>()
            };
            _context.Add(cart);
            await _context.SaveChangesAsync();
            return cart;
        }
        public async Task AddCartAsync(List<CartItem> newItems, string userId)
        {
            if (newItems != null)
            {
                var products = newItems.Select(x => x.Product).ToList();
                _context.Products.AttachRange(products);
                var cart = await GetByUserIdAsync(userId);
                if (cart == null)
                {
                    cart = new Cart
                    {
                        UserId = userId,
                        Items = new List<CartItem>(newItems)
                    };
                    _context.Carts.Add(cart);
                }
                await _context.SaveChangesAsync();
            }
        }
    }
}
