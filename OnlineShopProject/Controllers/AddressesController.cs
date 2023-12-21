using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopProject.Interfaces;
using OnlineShopProject.Models;
using System.Security.Claims;

namespace OnlineShopProject.Controllers
{
    [Authorize]
    public class AddressesController : Controller
    {
        private readonly IAddressesRepository _addressesRepository;
        private readonly IMapper _mapper;
        private readonly IUsersService _usersService;
        public AddressesController(IAddressesRepository addressesRepository, IMapper mapper, IUsersService usersService)
        {
            _addressesRepository = addressesRepository;
            _mapper = mapper;
            _usersService = usersService;
        }
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var addresses = _mapper.Map<DeliveryInfoViewModel>(await _addressesRepository.GetByUserIdAsync(userId));
            return View(addresses?.DeliveryInfoItems);
        }
        [HttpGet]
        public IActionResult CreateInfo()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateInfo(DeliveryInfoItemViewModel userAddress)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var info = _mapper.Map<DeliveryInfoItem>(userAddress);   
            await _addressesRepository.AddAsync(info, userId);
            return RedirectToAction("Index");
        }
        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditInfo(Guid id)
        {
            var address = _mapper.Map<DeliveryInfoItemViewModel>(await _addressesRepository.GetByIdAsync(id));
            return View(address);
        }
        [HttpPost]
        public async Task<IActionResult> EditInfo(DeliveryInfoItemViewModel userAddress)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception();
            }
            var address = _mapper.Map<DeliveryInfoItem>(userAddress);
            await _addressesRepository.EditAsync(address);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Remove(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _addressesRepository.RemoveAsync(id, userId);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> SetDefault(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _addressesRepository.SetCurrentAsync(userId, id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> GetDefault(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _addressesRepository.GetCurrentAsync(userId);
            return RedirectToAction("Index");
        }
    }
}
