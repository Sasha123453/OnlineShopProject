namespace OnlineShopProject.Models
{
    public class Order
    {
        public CartViewModel Cart { get; set; }
        public Guid Id { get; set; }
        public DeliveryInfoItemViewModel Info { get; set; }
    }
}
