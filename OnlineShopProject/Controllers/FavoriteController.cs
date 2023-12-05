using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShopProject.InMemoryModels;
using OnlineShopProject.Interfaces;
using OnlineShopProject.Models;

namespace OnlineShopProject.Controllers
{
    public class FavoriteController : Controller
    {
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IProductsRepository _productRepository;
        private readonly IConstances _constances;
        public FavoriteController(IFavoriteRepository favoriteRepository, IProductsRepository productsRepository, IConstances constances)
        {
            _favoriteRepository = favoriteRepository;
            _productRepository = productsRepository;
            _constances = constances;
        }
        public IActionResult Index()
        {
            var model = _favoriteRepository.GetAllUserFavorites(_constances.UserId);
            return View(model?.Products);
        }
        //public IActionResult AddToFavorites(Guid id)
        //{
        //    var product = _productRepository.GetProductById(id);
        //    _favoriteRepository.AddToFavorites(product, _constances.UserId);
        //    return RedirectToAction("ProductsPage", "Shop");
        //}
        //public IActionResult Delete(int id)
        //{
        //    var model = _productRepository.GetProductById(id);
        //    _favoriteRepository.Delete(model, _constances.UserId);
        //    return RedirectToAction("Index");
        //}
    }
}
