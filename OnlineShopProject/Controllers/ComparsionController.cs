using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopProject.Interfaces;
using OnlineShopProject.Models;
using System.Collections;
using System.Security.Claims;

namespace OnlineShopProject.Controllers
{
    [Authorize]
    public class ComparsionController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IComparsionRepository _comparsionRepository;
        private readonly IProductsRepository _productsRepository;
        private readonly UserManager<User> _userManager;
        public ComparsionController(IComparsionRepository comparsionRepository, IProductsRepository productsRepository, IMapper mapper, UserManager<User> userManager)
        {
            _comparsionRepository = comparsionRepository;
            _productsRepository = productsRepository;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<IActionResult> ComparsionPage()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var products = await _comparsionRepository.GetByUserIdAsync(userId);
            var model = _mapper.Map<IEnumerable<ProductViewModel>>(products);
            return View(model);
        }
        public async Task<IActionResult> AddToComparsion(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var product = await _productsRepository.GetByIdAsync(id);
            await _comparsionRepository.AddAsync(product, userId);
            return RedirectToAction("ProductsPage", "Shop");
        }
        public async Task<IActionResult> DeleteFromComparsions(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var product = await _productsRepository.GetByIdAsync(id);
            await _comparsionRepository.RemoveAsync(product.Id, userId);
            return RedirectToAction("ComparsionPage");
        }
    }
}
