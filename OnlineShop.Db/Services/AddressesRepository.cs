

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
        public DeliveryInfo GetAddresses(string userId)
        {
            return _context.DeliveryInfos.Include(x => x.DeliveryInfoItems).Where(x => x.UserId == userId).FirstOrDefault();
        }
        public DeliveryInfoItem GetAddressById(Guid id)
        {
            return _context.DeliveryInfoItems.Where(x => x.Id == id).FirstOrDefault();
        }
        public void AddAddress(DeliveryInfoItem address, string userId)
        {
            var addresses = GetAddresses(userId);
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
                    DeliveryInfo = model
                };
                model.DeliveryInfoItems = new List<DeliveryInfoItem> { item };
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
        public void RemoveAddress(Guid id)
        {
            var toRemove = GetAddressById(id);
            _context.Remove(toRemove);
        }
        public void EditAddress(DeliveryInfoItem address)
        {
            var toEdit = GetAddressById(address.Id);
            toEdit.Address = address.Address;
            toEdit.PhoneNumber = address.PhoneNumber;
            toEdit.FullName = address.FullName;
            _context.SaveChanges();
        }
    }
}
