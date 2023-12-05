using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopProject.Models;

namespace OnlineShopProject.Controllers
{
    public class AdminController : Controller
    {
        private readonly IProductsRepository _productRepository;
        public AdminController(IProductsRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public IActionResult Orders()
        {
            return View();
        }
        public IActionResult Products()
        {
            var products = ProductViewModel.ToProductViewModels(_productRepository.GetAllProducts());
            return View(products);
        }
        public IActionResult DeleteProduct(Guid id)
        {
            _productRepository.DeleteProduct(id);
            return RedirectToAction("Products");
        }
        public IActionResult EditPage(Guid id)
        {
            var product = _productRepository.GetProductById(id);
            ProductViewModel model = new ProductViewModel(product);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(int id, ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            _productRepository.EditProduct((Product)model);
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
            _productRepository.AddProduct((Product)model);
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
