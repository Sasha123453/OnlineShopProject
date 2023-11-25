using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Details(int id)
        {
            return View(_productRepository.GetProductById(id));
        }
    }
}
