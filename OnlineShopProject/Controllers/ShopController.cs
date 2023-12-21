using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> ProductsPage()
        {
            var model = _mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.GetAllAsync());
            return View(model);
        }
        public async Task<IActionResult> Search(string name)
        {
            var model = _mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.SearchAsync(name));
            return View("ProductsPage", model);
        }
        public async Task<IActionResult> Details(Guid id)
        {
            var productViewModel = _mapper.Map<ProductViewModel>(await _productRepository.GetByIdAsync(id));
            return View(productViewModel);
        }
    }
}
