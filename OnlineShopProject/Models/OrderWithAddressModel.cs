namespace OnlineShopProject.Models
{
    public class OrderWithAddressModel
    {
        public Cart Cart { get; set; }
        public AddressModel Address { get; set; }
    }
}
