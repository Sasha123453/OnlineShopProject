using Newtonsoft.Json;
using OnlineShop.Db.Models;

namespace OnlineShopProject.Interfaces
{
    public interface ISessionsRepository<T>
    {
        public T Get();
        public void Add(Product product);
        public void Save(T tItem);
        public int GetCount();
        public void Remove(Guid id);
        public void Clear();
    }
}
