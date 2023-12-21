using Newtonsoft.Json;
using OnlineShop.Db.Models;
using OnlineShopProject.Interfaces;
using System.Web;

namespace OnlineShopProject.Services
{
    public class SessionsCartRepository : ISessionsRepository<Cart>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SessionsCartRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Cart Get()
        {
            var cart = _httpContextAccessor.HttpContext.Session.GetString("Cart");
            return (cart is null) ? null : JsonConvert.DeserializeObject<Cart>(cart);
        }
        public void Add(Product product)
        {
            var cart = Get();
            if (cart is null)
            {
                var item = new CartItem
                {
                    Id = Guid.NewGuid(),
                    Product = product,
                    Amount = 1
                };
                cart = new Cart
                {
                    Id = Guid.NewGuid(),
                    Items = new List<CartItem> { item }
                };
            }
            else
            {
                var item = cart.Items.FirstOrDefault(x => x.Product.Id == product.Id);
                if (item is null)
                {
                    item = new CartItem
                    {
                        Id = Guid.NewGuid(),
                        Product = product,
                        Amount = 1
                    };
                    cart.Items.Add(item);
                }
                else
                {
                    item.Amount++;
                }
            }
            Save(cart);
        }
        public int GetCount()
        {
            var cart = Get();
            return (cart is null) ? 0 : cart.Items.Count;
        }
        public void Save(Cart cart)
        {
            _httpContextAccessor.HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
        }
        public void Clear()
        {
            _httpContextAccessor.HttpContext.Session.SetString("Cart", "");
        }

        public void Remove(Guid id)
        {
            var cart = Get();
            if (cart is not null)
            {
                var item = cart.Items.Where(x => x.ProductId == id).FirstOrDefault();
                if (item is not null)
                {
                    if (item.Amount == 1) cart.Items.Remove(item);
                    else item.Amount--;
                }
            }
            Save(cart);
        }
    }
}
