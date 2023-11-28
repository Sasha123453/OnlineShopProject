using Microsoft.AspNetCore.Mvc;
using OnlineShopProject.Interfaces;
using OnlineShopProject.Models;

namespace OnlineShopProject.Controllers
{
    public class CartController : Controller
    {
        private readonly IConstances _constances;
        private readonly ICartsRepository _cartsRepository;
        private readonly IProductsRepository _productsRepository;
        private readonly IOrdersRepository _ordersRepository;
        public CartController(IConstances constances, ICartsRepository cartsRepository, IProductsRepository productsRepository, IOrdersRepository ordersRepository)
        {
            _constances = constances;
            _cartsRepository = cartsRepository;
            _productsRepository = productsRepository;
            _ordersRepository = ordersRepository;
        }
        public IActionResult Index()
        {
            var x = _cartsRepository.GetCartByUserId(_constances.UserId);
            return View(x);
        }
        public IActionResult AddToCart(int id)
        {
            _cartsRepository.AddToCart(_constances.UserId, _productsRepository.GetProductById(id));  
            return RedirectToAction("Index");
        }
        public IActionResult ChangeAmount(int amount, int id)
        {
            _cartsRepository.ChangeAmount(amount, id, _constances.UserId);
            return RedirectToAction("Index");
        }
        public IActionResult ShowOrder()
        {
            var cart = _cartsRepository.GetCartByUserId(_constances.UserId);
            return View(cart);
        }
        public IActionResult CreateOrder(OrderInfoModel orderInfo)
        {
            var usercart = _cartsRepository.GetCartByUserId(_constances.UserId);
            _ordersRepository.AddToOrders(usercart, orderInfo);
            _cartsRepository.RemoveCart(_constances.UserId);
            return View("CreateOrder");
        }
    }
}
