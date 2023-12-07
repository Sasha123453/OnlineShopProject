using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShopProject.Models;

namespace OnlineShopProject.Controllers
{
    public class ShopController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProductsRepository _productRepository;
        public ShopController(IProductsRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public IActionResult ProductsPage()
        {
            var model = _mapper.Map<IEnumerable<ProductViewModel>>(_productRepository.GetAllProducts());
            return View(model);
        }
        public IActionResult Search(string name)
        {
            var model = _mapper.Map<IEnumerable<ProductViewModel>>(_productRepository.SearchProducts(name));
            return View("ProductsPage", model);
        }
        public IActionResult Details(Guid id)
        {
            var productViewModel = _mapper.Map<ProductViewModel>(_productRepository.GetProductById(id));
            return View(productViewModel);
        }
    }
}
