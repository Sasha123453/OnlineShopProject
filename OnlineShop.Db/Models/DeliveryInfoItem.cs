namespace OnlineShop.Db.Models
{
    public class DeliveryInfoItem
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public DeliveryInfo DeliveryInfo { get; set; }
    }
}
