using Microsoft.AspNetCore.Identity;

namespace OnlineShop.Db.Models
{
    public class User : IdentityUser
    {
        public DeliveryInfoItem? DeliveryInfoItem { get; set; }
        public Guid? DeliveryInfoItemId { get; set; }
    }
}
