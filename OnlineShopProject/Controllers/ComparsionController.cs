using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShopProject.Interfaces;
using OnlineShopProject.Models;
using System.Collections;

namespace OnlineShopProject.Controllers
{
    public class ComparsionController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IComparsionRepository _comparsionRepository;
        private readonly IConstances _constances;
        private readonly IProductsRepository _productsRepository;
        public ComparsionController(IComparsionRepository comparsionRepository, IConstances constances, IProductsRepository productsRepository, IMapper mapper)
        {
            _comparsionRepository = comparsionRepository;
            _constances = constances;
            _productsRepository = productsRepository;
            _mapper = mapper;
        }
        public IActionResult ComparsionPage()
        {
            var products = _comparsionRepository.GetUserComparsions(_constances.UserId);
            var model = _mapper.Map<IEnumerable<ProductViewModel>>(products);
            return View(model);
        }
        public IActionResult AddToComparsion(Guid id)
        {
            var product = _productsRepository.GetProductById(id);
            _comparsionRepository.AddToComparsion(product, _constances.UserId);
            return RedirectToAction("ProductsPage", "Shop");
        }
        public IActionResult DeleteFromComparsions(Guid id)
        {
            var product = _productsRepository.GetProductById(id);
            _comparsionRepository.Delete(product.Id, _constances.UserId);
            return RedirectToAction("ComparsionPage");
        }
    }
}
