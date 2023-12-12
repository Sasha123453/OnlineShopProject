using AutoMapper;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
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
        public IActionResult ShowOrder(List<Guid> ids)
        {
            var items = _cartsRepository.GetCartItemsByIds(ids, _constances.UserId);
            var info = _addressesRepository.GetCurrentUserInfo(_constances.UserId);
            var viewItems = _mapper.Map<List<CartItemViewModel>>(items);
            var viewInfo = _mapper.Map<DeliveryInfoItemViewModel>(info);
            OrderViewModel model = new OrderViewModel
            {
                CartItems = viewItems,
                Info = viewInfo
            };
            return View(model);
        }
        public IActionResult CreateOrder(List<Guid> ids)
        {
            var items = _cartsRepository.GetCartItemsByIds(ids, _constances.UserId);
            var info = _addressesRepository.GetCurrentUserInfo(_constances.UserId);
            if (info is null) throw new Exception("Адрес не может быть пустым");
            var order = new Order
            {
                CartItems = items,
                UserId = _constances.UserId,
                Info = info,
                Status = OrderStatus.Created
            };
            _ordersRepository.AddToOrders(order);
            _cartsRepository.DeleteCartItems(items,_constances.UserId);
            return View("CreateOrder");
        }
    }
}
