using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopProject.Interfaces;
using OnlineShopProject.Models;

namespace OnlineShopProject.Controllers
{
    [Authorize]
    public class FavoriteController : Controller
    {
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IProductsRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public FavoriteController(IFavoriteRepository favoriteRepository, IProductsRepository productsRepository, IMapper mapper, UserManager<User> userManager)
        {
            _favoriteRepository = favoriteRepository;
            _productRepository = productsRepository;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var model = _mapper.Map<IEnumerable<ProductViewModel>>(await _favoriteRepository.GetByUserIdAsync(user.Id));
            return View(model);
        }
        public async Task<IActionResult> AddToFavorites(Guid id)
        {
            var user = await _userManager.GetUserAsync(User);
            var product = await _productRepository.GetByIdAsync(id);
            await _favoriteRepository.AddAsync(product, user.Id);
            return RedirectToAction("ProductsPage", "Shop");
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _userManager.GetUserAsync(User);
            await _favoriteRepository.RemoveAsync(id, user.Id);
            return RedirectToAction("Index");
        }
    }
}
