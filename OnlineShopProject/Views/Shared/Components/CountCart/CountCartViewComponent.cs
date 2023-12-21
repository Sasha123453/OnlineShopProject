using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopProject.Interfaces;
using OnlineShopProject.Models;

namespace OnlineShopProject.Views.Shared.ViewComponents.CountCart
{
    public class CountCartViewComponent : ViewComponent
    {
        private readonly ICartsRepository _cartsRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly ISessionsRepository<Cart> _sessionCartsRepository;
        public CountCartViewComponent(ICartsRepository cartsRepository, IMapper mapper, UserManager<User> userManager, ISessionsRepository<Cart> sessionCartsRepository)
        {
            _cartsRepository = cartsRepository;
            _mapper = mapper;
            _userManager = userManager;
            _sessionCartsRepository = sessionCartsRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            int amount = 0;
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync((System.Security.Claims.ClaimsPrincipal)User);
                amount = await _cartsRepository.GetCountByUserIdAsync(user.Id);
            }
            else
            {
                amount = _sessionCartsRepository.GetCount();
            }
            return View("CountCart", amount);
        }
    }
}
