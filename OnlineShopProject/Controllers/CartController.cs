using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopProject.InMemoryModels;
using OnlineShopProject.Interfaces;
using OnlineShopProject.Models;

namespace OnlineShopProject.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartsRepository _cartsRepository;
        private readonly IProductsRepository _productsRepository;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IAddressesRepository _addressesRepository;
        private readonly IMapper _mapper;
        private readonly IUsersService _userManager;
        private readonly UserOrders userOrders;

        public CartController(ICartsRepository cartsRepository, IProductsRepository productsRepository, IOrdersRepository ordersRepository, UserOrders userOrders, IAddressesRepository addressesRepository, IMapper mapper, IUsersService userManager)
        {
            _cartsRepository = cartsRepository;
            _productsRepository = productsRepository;
            _ordersRepository = ordersRepository;
            this.userOrders = userOrders;
            _addressesRepository = addressesRepository;
            _mapper = mapper;
            _userManager = userManager;

        }
        public IActionResult Index()
        {
            var cart = _mapper.Map<CartViewModel>(_cartsRepository.GetByUserId(User.Identity.Name));
            return View(cart);
        }
        public IActionResult AddToCart(Guid id)
        {
            var model = _productsRepository.GetById(id);
            _cartsRepository.Add(User.Identity.Name, model);
            return Ok();
        }
        public IActionResult DecreaseAmount(Guid id)
        {
            _cartsRepository.DecreaseAmount(id, User.Identity.Name);
            return Ok();
        }
        public IActionResult ShowOrder(List<Guid> ids)
        {
            var items = _cartsRepository.GetByIds(ids, User.Identity.Name);
            var info = _userManager.GetCurrentDeliveryInfoItem(User.Identity.Name);
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
            var items = _cartsRepository.GetByIds(ids, User.Identity.Name);
            var info = _userManager.GetCurrentDeliveryInfoItem(User.Identity.Name);
            if (info is null) throw new Exception("Адрес не может быть пустым");
            var order = new Order
            {
                CartItems = items,
                UserId = User.Identity.Name,
                Info = info,
                Status = OrderStatus.Created
            };
            _ordersRepository.Add(order);
            _cartsRepository.RemoveItems(items,User.Identity.Name);
            return View("CreateOrder");
        }
    }
}
