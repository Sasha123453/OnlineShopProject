

using Microsoft.EntityFrameworkCore;
using OnlineShopProject.Interfaces;

namespace OnlineShop.Db.Models
{
    public class AddressesRepository : IAddressesRepository
    {
        private readonly ApplicationContext _context;
        private readonly IdentityContext _identityContext;
        public AddressesRepository(ApplicationContext context, IdentityContext identityContext)
        {
            _context = context;
            _identityContext = identityContext;
        }
        public async Task<DeliveryInfo> GetByUserIdAsync(string userId)
        {
             return await _context.DeliveryInfos.Include(x => x.DeliveryInfoItems).Where(x => x.UserId == userId).FirstOrDefaultAsync();
        }
        public async Task<DeliveryInfoItem> GetByIdAsync(Guid id)
        {
            return await _context.DeliveryInfoItems.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task AddAsync(DeliveryInfoItem address, string userId)
        {
            var addresses = await GetByUserIdAsync(userId);
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
            await _context.SaveChangesAsync();
        }
        public async Task RemoveAsync(Guid id, string userId)
        {
            var info = await GetByUserIdAsync(userId);      
            var toRemove = info.DeliveryInfoItems.FirstOrDefault(x => x.Id == id);
            info.DeliveryInfoItems.Remove(toRemove);
            await _context.SaveChangesAsync();
        }
        public async Task EditAsync(DeliveryInfoItem address)
        {
            await _context.DeliveryInfoItems.ExecuteUpdateAsync(x => 
            x.SetProperty(x => x.Address, address.Address)
            .SetProperty(x => x.PhoneNumber, address.PhoneNumber)
            .SetProperty(x => x.FullName, address.FullName));
        }
        public async Task<DeliveryInfoItem> GetCurrentAsync(string userId)
        {
            return await _identityContext.Users.Include(x => x.DeliveryInfoItem).Where(x => x.Id == userId).Select(x => x.DeliveryInfoItem).FirstOrDefaultAsync();
        }
        public async Task SetCurrentAsync(string userId, Guid deliveryInfoItemId)
        {
            await _identityContext.Users.Where(z => z.Id == userId).ExecuteUpdateAsync(x => x.SetProperty(y => y.DeliveryInfoItemId, deliveryInfoItemId));
        }
    }
}
