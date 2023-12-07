

namespace OnlineShop.Db.Models
{
    public class Favorite
    {
        public Guid Id { get; set; }
        public Product Product { get; set; }
        public string UserId { get; set; }
    }
}
