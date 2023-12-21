
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
        public async Task<Product> GetByIdAsync(Guid id)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }
        public async Task<List<Product>> SearchAsync(string name)
        {
            return await _context.Products.Where(x => x.Name == name).ToListAsync();
        }
        public async Task RemoveAsync(Guid id)
        {
            await _context.Products.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public async Task EditAsync(Product model)
        {
            await _context.Products.Where(x => x.Id == model.Id).ExecuteUpdateAsync(
                x => x.SetProperty(x => x.Name, model.Name)
                .SetProperty(x => x.Cpu, model.Cpu)
                .SetProperty(x => x.Description, model.Description)
                .SetProperty(x => x.Price, model.Price)
                .SetProperty(x => x.MaxFrequency, model.MaxFrequency)
                .SetProperty(x => x.Ram, model.Ram)
                .SetProperty(x => x.Category, model.Category));
        }
        public async Task AddAsync(Product model)
        {
            _context.Products.Add(model);
            await _context.SaveChangesAsync();
        }
        
    }
}
