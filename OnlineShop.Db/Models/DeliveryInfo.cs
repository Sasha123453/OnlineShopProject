

namespace OnlineShop.Db.Models
{
    public class DeliveryInfo
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public List<DeliveryInfoItem> DeliveryInfoItems { get; set; }
    }
}
