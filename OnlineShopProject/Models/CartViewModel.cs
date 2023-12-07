using OnlineShop.Db.Models;

namespace OnlineShopProject.Models
{
    public class CartViewModel
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public List<CartItemViewModel> Items { get; set;}
        public CartViewModel(string userId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Items = new List<CartItemViewModel>();
        }
        public CartViewModel()
        {

        }
        public decimal Cost { get
            {
                return Items?.Sum(x => x.Cost) ?? 0;
            } 
        }
        public decimal Amount
        {
            get
            {
                return Items?.Sum(x => x.Amount) ?? 0;
            }
        }
    }
}
