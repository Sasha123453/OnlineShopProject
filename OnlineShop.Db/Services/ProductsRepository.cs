
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;


namespace OnlineShop.Db.Services
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly ApplicationContext _context;
        public ProductsRepository(ApplicationContext context)
        {
            _context = context;
        }
        public Product GetById(Guid id)
        {
            return _context.Products.FirstOrDefault(x => x.Id == id);
        }
        public List<Product> GetAll()
        {
            return _context.Products.ToList();
        }
        public List<Product> Search(string name)
        {
            return _context.Products.Where(x => x.Name == name).ToList();
        }
        public void Remove(Guid id)
        {
            _context.Remove(_context.Products.FirstOrDefault(x => x.Id == id));
            _context.SaveChanges();
        }

        public void Edit(Product model)
        {
            var toEdit = _context.Products.AsNoTracking().FirstOrDefault(x => x.Id == model.Id);
            if (toEdit != null)
            {
                toEdit.Name = model.Name;
                toEdit.Description = model.Description;
                toEdit.Price = model.Price;
                toEdit.Category = model.Category;
                toEdit.Ram = model.Ram;
                toEdit.Cpu = model.Cpu;
                toEdit.CoresAmount = model.CoresAmount;
                toEdit.MaxFrequency = model.MaxFrequency;
            }
            _context.Update(toEdit);
            _context.SaveChanges();

        }
        public void Add(Product model)
        {
            _context.Products.Add(model);
            _context.SaveChanges();
        }
    }
}
