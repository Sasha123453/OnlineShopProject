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
        private readonly IProductsRepository _productsRepository;
        public ComparsionController(IComparsionRepository comparsionRepository, IProductsRepository productsRepository, IMapper mapper)
        {
            _comparsionRepository = comparsionRepository;
            _productsRepository = productsRepository;
            _mapper = mapper;
        }
        public IActionResult ComparsionPage()
        {
            var products = _comparsionRepository.GetByUserId(User.Identity.Name);
            var model = _mapper.Map<IEnumerable<ProductViewModel>>(products);
            return View(model);
        }
        public IActionResult AddToComparsion(Guid id)
        {
            var product = _productsRepository.GetById(id);
            _comparsionRepository.Add(product, User.Identity.Name);
            return RedirectToAction("ProductsPage", "Shop");
        }
        public IActionResult DeleteFromComparsions(Guid id)
        {
            var product = _productsRepository.GetById(id);
            _comparsionRepository.Remove(product.Id, User.Identity.Name);
            return RedirectToAction("ComparsionPage");
        }
    }
}
