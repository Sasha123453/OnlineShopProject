namespace OnlineShopProject.Models
{
    public class Cart
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public List<CartItem> Items { get; set;}
        public Cart(string userId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Items = new List<CartItem>();
        }
        public decimal Cost { get
            {
                decimal totalprice = 0;
                foreach (var item in Items)
                {
                    totalprice += item.Cost;
                }
                return totalprice;
            } 
        }
    }
}
