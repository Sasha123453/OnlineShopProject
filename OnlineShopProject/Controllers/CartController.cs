using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using OnlineShop.Db.Interfaces;
using OnlineShopProject.InMemoryModels;
using OnlineShopProject.Interfaces;
using OnlineShopProject.Models;
using System.Collections.Concurrent;
using System.Reflection.Metadata.Ecma335;

namespace OnlineShopProject.Controllers
{
    public class CartController : Controller
    {
        private readonly IConstances _constances;
        private readonly ICartsRepository _cartsRepository;
        private readonly IProductsRepository _productsRepository;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IAddressesRepository _addressesRepository;
        private readonly UserOrders userOrders;
        
        public CartController(IConstances constances, ICartsRepository cartsRepository, IProductsRepository productsRepository, IOrdersRepository ordersRepository, UserOrders userOrders, IAddressesRepository addressesRepository)
        {
            _constances = constances;
            _cartsRepository = cartsRepository;
            _productsRepository = productsRepository;
            _ordersRepository = ordersRepository;
            this.userOrders = userOrders;
            _addressesRepository = addressesRepository;

        }
        public IActionResult Index()
        {
            var x = _cartsRepository.GetCartByUserId(_constances.UserId);
            return View(x);
        }
        public IActionResult AddToCart(Guid id)
        {
            var model = new ProductViewModel(_productsRepository.GetProductById(id));
            _cartsRepository.AddToCart(_constances.UserId, model);  
            return RedirectToAction("Index");
        } 
        public IActionResult ChangeAmount(Guid id, int amount)
        {
            _cartsRepository.ChangeAmount(amount, id, _constances.UserId);
            userOrders.Update(_constances.UserId);
            return Ok();
        }
        //public IActionResult AddOrDeletePreorders(int id)
        //{
        //    userOrders.AddOrDeletePreorders(id, _constances.UserId);
        //    return Ok();
        //}
        public IActionResult ShowOrder()
        {
            (var cart, var address) = GetOrdersWithInfo();
            OrderWithAddressModel model = new OrderWithAddressModel
            {
                Cart = cart,
                Address = address
            };
            return View(model);
        }
        public IActionResult CreateOrder()
        {
            (var cart, var address) = GetOrdersWithInfo();
            if (address == null) throw new Exception();
            _ordersRepository.AddToOrders(cart, address);
            return View("CreateOrder");
        }
        [NonAction]
        public (Cart, AddressModel) GetOrdersWithInfo()
        {
            var cartItems = userOrders.GetOrdersById(_constances.UserId);
            Cart cart = new Cart(_constances.UserId);
            cart.Items = cartItems;
            var address = _addressesRepository?.GetAddresses(_constances.UserId)?.Addresses?[0];
            return (cart, address);
        }
    }
}
