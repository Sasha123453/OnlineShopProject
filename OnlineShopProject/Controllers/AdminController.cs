using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopProject.Interfaces;
using OnlineShopProject.Models;

namespace OnlineShopProject.Controllers
{
    public class AdminController : Controller
    {
        private readonly IProductsRepository _productRepository;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IConstances _constances;
        private readonly IMapper _mapper;
        public AdminController(IProductsRepository productRepository, IMapper mapper, IOrdersRepository ordersRepository,IConstances constances)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _ordersRepository = ordersRepository;
            _constances = constances;
        }
        public IActionResult Orders()
        {
            var orders = _ordersRepository.GetAll();
            var models = _mapper.Map<List<OrderViewModel>>(orders);
            return View(models);
        }
        [HttpGet]
        public IActionResult EditOrder(Guid id)
        {
            var order = _mapper.Map<OrderViewModel>(_ordersRepository.GetOrderById(id));
            return View(order);
        }
        [HttpPost]
        public IActionResult EditOrder(Guid orderId, OrderStatus status)
        {
            _ordersRepository.EditOrder(orderId, status);
            return RedirectToAction("Orders");
        }
        public IActionResult Products()
        {
            var products = _mapper.Map<IEnumerable<ProductViewModel>>(_productRepository.GetAllProducts());
            return View(products);
        }
        public IActionResult DeleteProduct(Guid id)
        {
            _productRepository.DeleteProduct(id);
            return RedirectToAction("Products");
        }
        public IActionResult EditProductPage(Guid id)
        {
            var model = _mapper.Map<ProductViewModel>(_productRepository.GetProductById(id));
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(int id, ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            _productRepository.EditProduct(_mapper.Map<Product>(model));
            return RedirectToAction("Products");
        }
        public IActionResult AddPage()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddProduct(ProductViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();
            _productRepository.AddProduct(_mapper.Map<Product>(model));
            return RedirectToAction("Products");
        }
        public IActionResult Roles()
        {
            return View();
        }
        public IActionResult Users()
        {
            return View();
        }
    }
}
