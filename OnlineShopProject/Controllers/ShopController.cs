using Microsoft.AspNetCore.Mvc;
using OnlineShopProject.Interfaces;
using OnlineShopProject.Models;

namespace OnlineShopProject.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductsRepository _productRepository;
        public ShopController(IProductsRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public IActionResult ProductsPage()
        {
            return View(_productRepository.GetAllProducts());
        }
        public IActionResult Search(string name)
        {
            var model = _productRepository.SearchProducts(name);
            return View("ProductsPage", model);
        }
        public IActionResult Details(int id)
        {
            return View(_productRepository.GetProductById(id));
        }
    }
}
