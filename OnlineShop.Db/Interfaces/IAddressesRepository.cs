

using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;

namespace OnlineShopProject.Interfaces
{
    public interface IAddressesRepository
    {
        public Task<DeliveryInfo> GetByUserIdAsync(string userId);
        public Task<DeliveryInfoItem> GetByIdAsync(Guid id);
        public Task AddAsync(DeliveryInfoItem address, string userId);
        public Task RemoveAsync(Guid id, string userId);
        public Task EditAsync(DeliveryInfoItem address);
        public Task<DeliveryInfoItem> GetCurrentAsync(string userId);
        public Task SetCurrentAsync(string userId, Guid deliveryInfoItemId);
    }
}
