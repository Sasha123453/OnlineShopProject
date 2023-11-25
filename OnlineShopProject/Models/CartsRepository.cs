namespace OnlineShopProject.Models
{
    public class CartsRepository : ICartsRepository
    {
        public List<Cart> Carts = new List<Cart>();
        public Cart GetCartByUserId(string userId)
        {
            return Carts.FirstOrDefault(x => x.UserId == userId);
        }
        public void AddToCart(string userId, ProductModel model)
        {
            Cart cart = GetCartByUserId(userId);
            if (cart == null)
            {
                cart = new Cart(userId);
                var cartitem = new CartItem(model, 1);
                cart.Items.Add(cartitem);
                Carts.Add(cart);
            }
            else
            {
                var item = cart.Items.FirstOrDefault(x => x.Product.Id == model.Id);
                if (item == null)
                {
                    CartItem newitem = new CartItem(model, 1);
                    cart.Items.Add(newitem);
                }
                else
                {
                    item.Amount++;
                }
            }
        }
        public void ChangeAmount(int change, int id, string userId)
        {
            if (!(change == 1 || change == -1)) throw new Exception("Что-то пошло не так");
            var cart = GetCartByUserId(userId);
            var toChange = cart.Items.FirstOrDefault(x => x.Product.Id == id);
            toChange.Amount += change;
        }
        public Cart RemoveCart(string userId)
        {
            var cart = GetCartByUserId(userId);
            if (cart != null) Carts.Remove(cart);
            return cart;
        }
    }
}
   