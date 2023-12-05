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
    }
}
