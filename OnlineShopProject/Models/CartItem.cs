namespace OnlineShopProject.Models
{
    public class CartItem
    {
        public Guid Id { get; set; }
        public ProductModel Product { get; set; }
        public int Amount {  get; set; }
        public Decimal Cost { get
            {
                return Amount * Product.Price;
            }
        }
        public CartItem(ProductModel product, int amount)
        {
            Id = Guid.NewGuid();
            Product = product;
            Amount = amount;
        }

    }
}
