using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopProject.Models;

namespace OnlineShopProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IMapper _mapper;
        public ProductController(IProductsRepository productsRepository, IMapper mapper)
        {
            _productsRepository = productsRepository;
            _mapper = mapper;
        }
        public IActionResult Products()
        {
            var products = _mapper.Map<IEnumerable<ProductViewModel>>(_productsRepository.GetAll());
            return View(products);
        }
        public IActionResult DeleteProduct(Guid id)
        {
            _productsRepository.Remove(id);
            return RedirectToAction("Products");
        }
        [HttpGet]
        public IActionResult EditProduct(Guid id)
        {
            var model = _mapper.Map<ProductViewModel>(_productsRepository.GetById(id));
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(int id, ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            _productsRepository.Edit(_mapper.Map<Product>(model));
            return RedirectToAction("Products");
        }
        [HttpPost]
        public IActionResult AddProduct(ProductViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();
            _productsRepository.Add(_mapper.Map<Product>(model));
            return RedirectToAction("Products");
        }
    }
}
