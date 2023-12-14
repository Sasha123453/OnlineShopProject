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
        private readonly IMapper _mapper;
        public FavoriteController(IFavoriteRepository favoriteRepository, IProductsRepository productsRepository, IMapper mapper)
        {
            _favoriteRepository = favoriteRepository;
            _productRepository = productsRepository;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var model = _mapper.Map<IEnumerable<ProductViewModel>>(_favoriteRepository.GetByUserId(User.Identity.Name));
            return View(model);
        }
        public IActionResult AddToFavorites(Guid id)
        {
            var product = _productRepository.GetById(id);
            _favoriteRepository.Add(product, User.Identity.Name);
            return RedirectToAction("ProductsPage", "Shop");
        }
        public IActionResult Delete(Guid id)
        {
            _favoriteRepository.Remove(id, User.Identity.Name);
            return RedirectToAction("Index");
        }
    }
}
