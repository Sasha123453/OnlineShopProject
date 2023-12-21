using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;
using OnlineShopProject.Interfaces;
using OnlineShopProject.Models;
using System.Security.Claims;

namespace OnlineShopProject.Views.Shared.Components.ShowInfo
{
    public class ShowInfoViewComponent : ViewComponent
    {
        private readonly IAddressesRepository _addressesRepository;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public ShowInfoViewComponent(IAddressesRepository addressesRepository, UserManager<User> userManager, IMapper mapper)
        {
            _addressesRepository = addressesRepository;
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = (await _userManager.GetUserAsync((ClaimsPrincipal)User)).Id;
            var addresses = _mapper.Map<DeliveryInfoViewModel>(await _addressesRepository.GetByUserIdAsync(userId));
            var curAddress = (await _addressesRepository.GetCurrentAsync(userId)).Id;
            var viewModel = (addresses is null) ? null : new DeliveryInfoPageViewModel
            {
                CurrentId = curAddress,
                Items = addresses.DeliveryInfoItems,
            };
            return View("ShowInfo", viewModel);
        }
    }
}
