

using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;

namespace OnlineShopProject.Interfaces
{
    public interface IAddressesRepository
    {
        public DeliveryInfo GetAddresses(string userId);
        public DeliveryInfoItem GetAddressById(Guid id);
        public void AddAddress(DeliveryInfoItem address, string userId);
        public void RemoveAddress(Guid id, string userId);
        public DeliveryInfoItem GetCurrentUserInfo(string userId);
        public void EditAddress(DeliveryInfoItem address);
    }
}
