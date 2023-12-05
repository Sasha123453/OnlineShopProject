using OnlineShopProject.Models;

namespace OnlineShopProject.Interfaces
{
    public interface IAddressesRepository
    {
        public UserAddressesModel GetAddresses(string userId);
        public AddressModel GetAddressById(Guid id);
        public void AddAddress(AddressModel address, string userId);
        public void RemoveAddress(AddressModel address, string userId);
        public void EditAddress(AddressModel address);
    }
}
