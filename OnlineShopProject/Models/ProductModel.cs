namespace OnlineShopProject.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Category { get; set; }
        public int Ram { get; set; }
        public string Cpu { get; set; }
        public int CoresAmount { get; set; }
        public double MaxFrequency { get; set; }
        public ProductModel(int id, string name, string description, string category, int price, int ram, string cpu, int coresAmount, double maxFrequency)
        {
            Id = id;
            Category = category;
            Name = name;
            Description = description;
            Price = price;
            Ram = ram;
            Cpu = cpu;
            CoresAmount = coresAmount;
            MaxFrequency = maxFrequency;
        }
        public ProductModel()
        {

        }
    }
}
