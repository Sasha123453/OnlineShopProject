namespace OnlineShopProject.Models
{
    public class OrderWithAddressModel
    {
        public CartViewModel Cart { get; set; }
        public DeliveryInfoItemViewModel Address { get; set; }
    }
}
