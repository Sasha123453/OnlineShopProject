using OnlineShop.Db.Models;

namespace OnlineShopProject.Models
{
    public class CartItemViewModel
    {
        public Guid Id { get; set; }
        public ProductViewModel Product { get; set; }
        public Guid ProductId { get; set; }
        public int Amount {  get; set; }
        public Decimal Cost { get
            {
                return Amount * Product.Price;
            }
        }
        public CartItemViewModel()
        {

        }
        public CartItemViewModel(ProductViewModel product, int amount)
        {
            Id = Guid.NewGuid();
            Product = product;
            Amount = amount;
        }

    }
}
