

using Microsoft.EntityFrameworkCore;
using OnlineShopProject.Interfaces;

namespace OnlineShop.Db.Models
{
    public class AddressesRepository : IAddressesRepository
    {
        private readonly ApplicationContext _context;
        public AddressesRepository(ApplicationContext context)
        {
            _context = context;
        }
        public DeliveryInfo GetByUserId(string userId)
        {
             return _context.DeliveryInfos.Include(x => x.DeliveryInfoItems).Where(x => x.UserId == userId).FirstOrDefault();
        }
        public DeliveryInfoItem GetById(Guid id)
        {
            return _context.DeliveryInfoItems.Where(x => x.Id == id).FirstOrDefault();
        }
        public void Add(DeliveryInfoItem address, string userId)
        {
            var addresses = GetByUserId(userId);
            if (addresses == null)
            {
                var model = new DeliveryInfo
                {
                    UserId = userId,
                };
                var item = new DeliveryInfoItem
                {
                    PhoneNumber = address.PhoneNumber,
                    FullName = address.FullName,
                    Address = address.Address,
                };
                model.DeliveryInfoItems = new List<DeliveryInfoItem> { item };
                _context.DeliveryInfos.Add(model);
            }
            else
            {
                if (!addresses.DeliveryInfoItems.Contains(address))
                {
                    addresses.DeliveryInfoItems.Add(address);
                }
            }
            _context.SaveChanges();
        }
        public void Remove(Guid id, string userId)
        {
            var info = GetByUserId(userId);
            var toRemove = info.DeliveryInfoItems.FirstOrDefault(x => x.Id == id);
            info.DeliveryInfoItems.Remove(toRemove);
            _context.SaveChanges();
        }
        public void Edit(DeliveryInfoItem address)
        {
            var toEdit = GetById(address.Id);
            toEdit.Address = address.Address;
            toEdit.PhoneNumber = address.PhoneNumber;
            toEdit.FullName = address.FullName;
            _context.SaveChanges();
        }
    }
}
