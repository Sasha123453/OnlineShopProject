namespace OnlineShopProject.Models
{
    public class Order
    {
        public Cart Cart { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; } 
    }
}
