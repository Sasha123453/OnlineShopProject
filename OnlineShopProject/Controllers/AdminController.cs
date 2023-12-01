using Microsoft.AspNetCore.Mvc;
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
            var products = _productRepository.GetAllProducts();
            return View(products);
        }
        public IActionResult DeleteProduct(int id)
        {
            _productRepository.DeleteProduct(id);
            return RedirectToAction("Products");
        }
        public IActionResult EditPage(int id)
        {
            var product = _productRepository.GetProductById(id);
            return View(product);
        }
        [HttpPost]
        public IActionResult Edit(int id, ProductModel model)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            _productRepository.EditProduct(model);
            return RedirectToAction("Products");
        }
        public IActionResult AddPage()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(ProductModel model)
        {
            if (!ModelState.IsValid) return BadRequest();
            _productRepository.AddProduct(model);
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
