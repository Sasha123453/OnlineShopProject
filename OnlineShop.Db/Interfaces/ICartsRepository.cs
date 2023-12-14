

using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Interfaces
{
    public interface ICartsRepository
    {
        public Cart GetByUserId(string userId);
        public void Add(string userId, Product model);
        public void DecreaseAmount(Guid id, string userId);
        public List<CartItem> GetByIds(List<Guid> ids, string userId);
        public void RemoveItems(List<CartItem> items, string userId);
        public int GetCountByUserId(string userId);
    }
}
