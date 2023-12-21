using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopProject.Interfaces;
using OnlineShopProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace OnlineShopProject.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartsRepository _cartsRepository;
        private readonly IProductsRepository _productsRepository;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IAddressesRepository _addressesRepository;
        private readonly IMapper _mapper;
        private readonly ISessionsRepository<Cart> _sessionCartsRepository;
        private readonly IUsersService _userService;
        private readonly UserManager<User> _userManager;

        public CartController(ICartsRepository cartsRepository, IProductsRepository productsRepository, IOrdersRepository ordersRepository, IAddressesRepository addressesRepository, IMapper mapper, IUsersService userService, UserManager<User> userManager, ISessionsRepository<Cart> sessionCartsRepository)
        {
            _cartsRepository = cartsRepository;
            _productsRepository = productsRepository;
            _ordersRepository = ordersRepository;
            _addressesRepository = addressesRepository;
            _mapper = mapper;
            _userService = userService;
            _userManager = userManager;
            _sessionCartsRepository = sessionCartsRepository;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var cart = (User.Identity.IsAuthenticated) ? await _cartsRepository.GetByUserIdAsync(user.Id) : _sessionCartsRepository.Get();
            var cartViewModel = _mapper.Map<CartViewModel>(cart);
            return View(cartViewModel);
        }
        public async Task<IActionResult> AddToCart(Guid id)
        {
            var model = await _productsRepository.GetByIdAsync(id);
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                await _cartsRepository.AddAsync(user.Id, model);
            }
            else
            {
                _sessionCartsRepository.Add(model);
            }
            return Ok();
        }
        public async Task<IActionResult> DecreaseAmount(Guid id)
        {
            
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _cartsRepository.DecreaseAmountAsync(id, userId);
            }
            else
            {
                _sessionCartsRepository.Remove(id);
            }
            return Ok();
        }
        [Authorize]
        public async Task<IActionResult> ShowOrder(List<Guid> ids)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var items = await _cartsRepository.GetByIdsAsync(ids, userId);
            var info = await _addressesRepository.GetCurrentAsync(userId);
            var viewItems = _mapper.Map<List<CartItemViewModel>>(items);
            var viewInfo = _mapper.Map<DeliveryInfoItemViewModel>(info);
            OrderViewModel model = new OrderViewModel
            {
                CartItems = viewItems,
                Info = viewInfo
            };
            return View(model);
        }
        [Authorize]
        public async Task<IActionResult> CreateOrder(List<Guid> ids)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var items = await _cartsRepository.GetByIdsAsync(ids, userId);
            var info = await _addressesRepository.GetCurrentAsync(userId);
            if (info is null) throw new Exception("Адрес не может быть пустым");
            var order = new Order
            {
                CartItems = items,
                UserId = userId,
                Info = info,
                Status = OrderStatus.Created
            };
            await _ordersRepository.AddAsync(order);
            await _cartsRepository.RemoveItemsAsync(items, userId);
            return View();
        }
    }
}
