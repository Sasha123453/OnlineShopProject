using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopProject.Models;

namespace OnlineShopProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Constants.DefaultAdminRole)]
    public class ProductController : Controller
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IMapper _mapper;
        public ProductController(IProductsRepository productsRepository, IMapper mapper)
        {
            _productsRepository = productsRepository;
            _mapper = mapper;
        }
        public async Task<IActionResult> Products()
        {
            var products = _mapper.Map<IEnumerable<ProductViewModel>>(await _productsRepository.GetAllAsync());
            return View(products);
        }
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            await _productsRepository.RemoveAsync(id);
            return RedirectToAction("Products");
        }
        [HttpGet]
        public async Task<IActionResult> EditProduct(Guid id)
        {
            var model = _mapper.Map<ProductViewModel>(await _productsRepository.GetByIdAsync(id));
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditProduct(Guid id, ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            if (model.Id != id)
            {
                return BadRequest();
            }
            await _productsRepository.EditAsync(_mapper.Map<Product>(model));
            return RedirectToAction("Products");
        }
        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();
            await _productsRepository.AddAsync(_mapper.Map<Product>(model));
            return RedirectToAction("Products");
        }
    }
}
