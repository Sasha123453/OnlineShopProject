using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
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
            return View(ProductViewModel.ToProductViewModels(_productRepository.GetAllProducts()));
        }
        public IActionResult Search(string name)
        {
            var model = _productRepository.SearchProducts(name);
            var productViewModel = ProductViewModel.ToProductViewModels(model);
            return View("ProductsPage", productViewModel);
        }
        public IActionResult Details(Guid id)
        {
            var productViewModel = new ProductViewModel(_productRepository.GetProductById(id));
            return View(productViewModel);
        }
    }
}
