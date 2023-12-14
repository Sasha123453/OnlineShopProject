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
        private readonly IMapper _mapper;
        public CartViewComponent(ICartsRepository cartsRepository, IMapper mapper)
        {
            _cartsRepository = cartsRepository;
            _mapper = mapper;
        }
        public IViewComponentResult Invoke()
        {

            var amount = _cartsRepository.GetCountByUserId(User.Identity.Name);
            return View("Cart", amount);
        }
    }
}
