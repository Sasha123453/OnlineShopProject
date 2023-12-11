

namespace OnlineShop.Db.Models
{
    public class Favorite
    {
        public Guid Id { get; set; }
        public Product Product { get; set; }
        public Guid ProductId { get; set; }
        public string UserId { get; set; }
    }
}
