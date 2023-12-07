using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopProject.Models;

namespace OnlineShopProject.Controllers
{
    public class AdminController : Controller
    {
        private readonly IProductsRepository _productRepository;
        private readonly IMapper _mapper;
        public AdminController(IProductsRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public IActionResult Orders()
        {
            return View();
        }
        public IActionResult Products()
        {
            var products = _mapper.Map<IEnumerable<ProductViewModel>>(_productRepository.GetAllProducts());
            return View(products);
        }
        public IActionResult DeleteProduct(Guid id)
        {
            _productRepository.DeleteProduct(id);
            return RedirectToAction("Products");
        }
        public IActionResult EditPage(Guid id)
        {
            var model = _mapper.Map<ProductViewModel>(_productRepository.GetProductById(id));
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(int id, ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            _productRepository.EditProduct(_mapper.Map<Product>(model));
            return RedirectToAction("Products");
        }
        public IActionResult AddPage()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(ProductViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();
            _productRepository.AddProduct(_mapper.Map<Product>(model));
            return RedirectToAction("Products");
        }
        public IActionResult Roles()
        {
            return View();
        }
        public IActionResult Users()
        {
            return View();
        }
    }
}
