
using OnlineShop.Db.Models;
namespace OnlineShop.Db.Interfaces
{
    public interface IUsersService
    {
        public DeliveryInfoItem GetCurrentDeliveryInfoItem(string userId);
        public void SetCurrentDeliveryInfoItem(string userId, Guid deliveryInfoItemId);
    }
}
