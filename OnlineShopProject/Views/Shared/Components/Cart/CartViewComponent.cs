using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShopProject.Interfaces;
using OnlineShopProject.Models;

namespace OnlineShopProject.Views.Shared.ViewComponents.Cart
{
    public class CartViewComponent : ViewComponent
    {
        private readonly ICartsRepository _cartsRepository;
        private readonly IConstances _constances;
        private readonly IMapper _mapper;
        public CartViewComponent(ICartsRepository cartsRepository, IConstances constances, IMapper mapper)
        {
            _cartsRepository = cartsRepository;
            _constances = constances;
            _mapper = mapper;
        }
        public IViewComponentResult Invoke()
        {

            var cart = _cartsRepository.GetCartByUserId(_constances.UserId);
            CartViewModel cartViewModel = _mapper.Map<CartViewModel>(cart);
            return View("Cart", cartViewModel?.Amount);
        }
    }
}
