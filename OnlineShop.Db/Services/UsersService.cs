using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using Microsoft.AspNetCore.Identity;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopProject.Models;


namespace OnlineShop.Db.Services
{
    public class UsersService : IUsersService
    {
        private readonly IdentityContext _context;
        
        public UsersService(IdentityContext context)
        {
            _context = context;
        }
        public DeliveryInfoItem GetCurrentDeliveryInfoItem(string userId)
        {
            return _context.Users.Include(x => x.DeliveryInfoItem).Where(x => x.Id == userId).Select(x => x.DeliveryInfoItem).FirstOrDefault();
        }
        public void SetCurrentDeliveryInfoItem(string userId, Guid deliveryInfoItemId)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == userId);
            user.DeliveryInfoItemId = deliveryInfoItemId;
            _context.SaveChanges();
        }
    }
}
