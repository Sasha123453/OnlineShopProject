namespace OnlineShop.Db.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Category { get; set; }
        public int Ram { get; set; }
        public string Cpu { get; set; }
        public int CoresAmount { get; set; }
        public double MaxFrequency { get; set; }
        public List<CartItem> CartItems { get; set; }
        public List<Comparsion> Comparsions { get; set; }
        public List<Favorite> Favorites { get; set; }
        public Product()
        {
            CartItems = new List<CartItem>();
            Comparsions = new List<Comparsion>();
            Favorites = new List<Favorite>();
        }
    }
}
