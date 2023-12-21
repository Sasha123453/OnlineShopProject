
using OnlineShop.Db.Models;
namespace OnlineShop.Db.Interfaces
{
    public interface IUsersService
    {
        public Task<DeliveryInfoItem> GetCurrentAsync(string userId);
        public Task SetCurrentAsync(string userId, Guid deliveryInfoItemId);
    }
}
