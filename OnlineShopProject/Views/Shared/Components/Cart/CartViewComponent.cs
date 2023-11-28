using Microsoft.AspNetCore.Mvc;
using OnlineShopProject.Interfaces;

namespace OnlineShopProject.Views.Shared.ViewComponents.Cart
{
    public class CartViewComponent : ViewComponent
    {
        private readonly ICartsRepository _cartsRepository;
        private readonly IConstances _constances;
        public CartViewComponent(ICartsRepository cartsRepository, IConstances constances)
        {
            _cartsRepository = cartsRepository;
            _constances = constances;
        }
        public IViewComponentResult Invoke()
        {
            var rep = _cartsRepository.GetCartByUserId(_constances.UserId);
            return View("Cart", rep?.Amount);
        }
    }
}
