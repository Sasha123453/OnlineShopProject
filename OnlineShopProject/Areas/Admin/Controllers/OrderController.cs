using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopProject.Interfaces;
using OnlineShopProject.Models;

namespace OnlineShopProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Constants.DefaultAdminRole)]
    public class OrderController : Controller
    {
        private readonly IProductsRepository _productRepository;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IMapper _mapper;
        public OrderController(IProductsRepository productRepository, IMapper mapper, IOrdersRepository ordersRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _ordersRepository = ordersRepository;
        }
        public IActionResult Orders()
        {
            var orders = _ordersRepository.GetAllAsync();
            var models = _mapper.Map<List<OrderViewModel>>(orders);
            return View(models);
        }
        [HttpGet]
        public IActionResult EditOrder(Guid id)
        {
            var order = _mapper.Map<OrderViewModel>(_ordersRepository.GetByIdAsync(id));
            return View(order);
        }
        [HttpPost]
        public IActionResult EditOrder(Guid orderId, OrderStatus status)
        {
            _ordersRepository.EditAsync(orderId, status);
            return RedirectToAction("Orders");
        }
        
        
    }
}
