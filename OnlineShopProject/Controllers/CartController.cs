using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShopProject.InMemoryModels;
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
        private readonly IAddressesRepository _addressesRepository;
        private readonly IMapper _mapper;
        private readonly UserOrders userOrders;

        public CartController(IConstances constances, ICartsRepository cartsRepository, IProductsRepository productsRepository, IOrdersRepository ordersRepository, UserOrders userOrders, IAddressesRepository addressesRepository, IMapper mapper)
        {
            _constances = constances;
            _cartsRepository = cartsRepository;
            _productsRepository = productsRepository;
            _ordersRepository = ordersRepository;
            this.userOrders = userOrders;
            _addressesRepository = addressesRepository;
            _mapper = mapper;

        }
        public IActionResult Index()
        {
            var cart = _mapper.Map<CartViewModel>(_cartsRepository.GetCartByUserId(_constances.UserId));
            return View(cart);
        }
        public IActionResult AddToCart(Guid id)
        {
            var model = _productsRepository.GetProductById(id);
            _cartsRepository.AddToCart(_constances.UserId, model);
            return Ok();
        }
        public IActionResult DecreaseAmount(Guid id)
        {
            _cartsRepository.DecreaseAmount(id, _constances.UserId);
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
        public (CartViewModel, DeliveryInfoItemViewModel) GetOrdersWithInfo()
        {
            var cartItems = userOrders.GetOrdersById(_constances.UserId);
            CartViewModel cart = new CartViewModel(_constances.UserId);
            cart.Items = cartItems;
            var address = _addressesRepository?.GetAddresses(_constances.UserId).DeliveryInfoItems[0];
            var model = _mapper.Map<DeliveryInfoItemViewModel>(address);
            return (cart, model);
        }
    }
}
