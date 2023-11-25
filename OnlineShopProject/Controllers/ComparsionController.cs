using Microsoft.AspNetCore.Mvc;

namespace OnlineShopProject.Controllers
{
    public class ComparsionController : Controller
    {
        private readonly IComparsionRepository _comparsionRepository;
        private readonly IConstances _constances;
        private readonly IProductsRepository _productsRepository;
        public ComparsionController(IComparsionRepository comparsionRepository, IConstances constances, IProductsRepository productsRepository)
        {
            _comparsionRepository = comparsionRepository;
            _constances = constances;
            _productsRepository = productsRepository;
        }
        public IActionResult ComparsionPage()
        {
            var comparsion = _comparsionRepository.GetAllUserComparsions(_constances.UserId);
            return View(comparsion.Products);
        }
        public IActionResult AddToComparsion(int id)
        {
            var product = _productsRepository.GetProductById(id);
            _comparsionRepository.AddToComparsion(product, _constances.UserId);
            return RedirectToAction("ProductsPage", "Shop");
        }
    }
}
