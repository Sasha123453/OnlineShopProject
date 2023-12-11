using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;
using OnlineShopProject.Interfaces;
using OnlineShopProject.Models;

namespace OnlineShopProject.Controllers
{
    public class AddressesController : Controller
    {
        private readonly IAddressesRepository _addressesRepository;
        private readonly IConstances _constances;
        private readonly IMapper _mapper;
        public AddressesController(IAddressesRepository addressesRepository, IConstances constances, IMapper mapper)
        {
            _addressesRepository = addressesRepository;
            _constances = constances;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var addresses = _mapper.Map<DeliveryInfoViewModel>(_addressesRepository.GetAddresses(_constances.UserId));
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
            _addressesRepository.AddAddress(info, _constances.UserId);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var address = _addressesRepository.GetAddressById(id);
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
            _addressesRepository.EditAddress(address);
            return RedirectToAction("Index");
        }
        public IActionResult Remove(Guid id)
        {
            _addressesRepository.RemoveAddress(id, _constances.UserId);
            return RedirectToAction("Index");
        }
    }
}
