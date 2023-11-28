namespace OnlineShopProject.Models
{
    public class Order
    {
        public Cart Cart { get; set; }
        public Guid Id { get; set; }
        public OrderInfoModel Info { get; set; }
    }
}
