

using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;

namespace OnlineShopProject.Interfaces
{
    public interface IAddressesRepository
    {
        public DeliveryInfo GetByUserId(string userId);
        public DeliveryInfoItem GetById(Guid id);
        public void Add(DeliveryInfoItem address, string userId);
        public void Remove(Guid id, string userId);
        public void Edit(DeliveryInfoItem address);
    }
}
