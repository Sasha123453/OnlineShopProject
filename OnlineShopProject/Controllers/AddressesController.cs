using Microsoft.AspNetCore.Mvc;
using OnlineShopProject.Interfaces;
using OnlineShopProject.Models;

namespace OnlineShopProject.Controllers
{
    public class AddressesController : Controller
    {
        private readonly IAddressesRepository _addressesRepository;
        private readonly IConstances _constances;
        public AddressesController(IAddressesRepository addressesRepository, IConstances constances)
        {
            _addressesRepository = addressesRepository;
            _constances = constances;
        }
        public IActionResult Index()
        {
            var addresses = _addressesRepository.GetAddresses(_constances.UserId);
            return View(addresses?.Addresses);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(AddressModel userAddress)
        {
            _addressesRepository.AddAddress(userAddress, _constances.UserId);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var address = _addressesRepository.GetAddressById(id);
            return View(address);
        }
        [HttpPost]
        public IActionResult Edit(AddressModel userAddress)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception();
            }
            _addressesRepository.EditAddress(userAddress);
            return RedirectToAction("Index");
        }
    }
}
