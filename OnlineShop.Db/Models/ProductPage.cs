namespace OnlineShop.Db.Models
{
    public class ProductPage
    {
        public Product Product { get; set; }
        public bool IsInCart { get; set; }
        public bool IsInFavorite { get; set; }
        public bool IsInComparsion { get; set; }
    }
}
