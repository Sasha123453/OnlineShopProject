using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;
using OnlineShopProject.Interfaces;
using OnlineShopProject.Models;

namespace OnlineShopProject.Controllers
{
    [Authorize]
    public class AddressesController : Controller
    {
        private readonly IAddressesRepository _addressesRepository;
        private readonly IMapper _mapper;
        public AddressesController(IAddressesRepository addressesRepository, IMapper mapper)
        {
            _addressesRepository = addressesRepository;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var addresses = _mapper.Map<DeliveryInfoViewModel>(_addressesRepository.GetByUserId(User.Identity.Name));
            return View(addresses?.DeliveryInfoItems);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(DeliveryInfoItemViewModel userAddress)
        {
            var info = _mapper.Map<DeliveryInfoItem>(userAddress);   
            _addressesRepository.Add(info, User.Identity.Name);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var address = _addressesRepository.GetById(id);
            return View(address);
        }
        [HttpPost]
        public IActionResult Edit(DeliveryInfoItemViewModel userAddress)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception();
            }
            var address = _mapper.Map<DeliveryInfoItem>(userAddress);
            _addressesRepository.Edit(address);
            return RedirectToAction("Index");
        }
        public IActionResult Remove(Guid id)
        {
            _addressesRepository.Remove(id, User.Identity.Name);
            return RedirectToAction("Index");
        }
    }
}
