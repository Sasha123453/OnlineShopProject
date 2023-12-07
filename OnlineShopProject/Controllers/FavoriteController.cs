using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShopProject.Interfaces;
using OnlineShopProject.Models;

namespace OnlineShopProject.Controllers
{
    public class FavoriteController : Controller
    {
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IProductsRepository _productRepository;
        private readonly IConstances _constances;
        private readonly IMapper _mapper;
        public FavoriteController(IFavoriteRepository favoriteRepository, IProductsRepository productsRepository, IConstances constances, IMapper mapper)
        {
            _favoriteRepository = favoriteRepository;
            _productRepository = productsRepository;
            _constances = constances;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var model = _mapper.Map<IEnumerable<ProductViewModel>>(_favoriteRepository.GetUserFavorites(_constances.UserId));
            return View(model);
        }
        public IActionResult AddToFavorites(Guid id)
        {
            var product = _productRepository.GetProductById(id);
            _favoriteRepository.AddToFavorites(product, _constances.UserId);
            return RedirectToAction("ProductsPage", "Shop");
        }
        public IActionResult Delete(Guid id)
        {
            _favoriteRepository.Delete(id, _constances.UserId);
            return RedirectToAction("Index");
        }
    }
}
