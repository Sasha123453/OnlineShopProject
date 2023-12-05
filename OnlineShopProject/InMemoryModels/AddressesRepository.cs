using OnlineShopProject.Interfaces;
using OnlineShopProject.Models;

namespace OnlineShopProject.InMemoryModels
{
    public class AddressesRepository : IAddressesRepository
    {
        public List<UserAddressesModel> Addresses;
        public AddressesRepository()
        {
            Addresses = new List<UserAddressesModel>();
        }
        public UserAddressesModel GetAddresses(string userId)
        {
            return Addresses.FirstOrDefault(x => x.UserId == userId);
        }
        public AddressModel GetAddressById(Guid id)
        {
            return Addresses.SelectMany(x => x.Addresses).FirstOrDefault(x => x.Id == id);
        }
        public void AddAddress(AddressModel address, string userId)
        {
            address.Id = Guid.NewGuid();
            var addresses = GetAddresses(userId);
            if (addresses == null)
            {
                var model = new UserAddressesModel
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Addresses = new List<AddressModel>()
                };
                model.Addresses.Add(address);
                Addresses.Add(model);
            }
            else
            {
                addresses.Addresses.Add(address);
            }
        }
        public void RemoveAddress(AddressModel address, string userId)
        {
            var addresses = GetAddresses(userId);
            addresses.Addresses.Remove(address);
        }
        public void EditAddress(AddressModel address)
        {
            var toEdit = GetAddressById(address.Id);
            toEdit.Address = address.Address;
            toEdit.PhoneNumber = address.PhoneNumber;
            toEdit.FullName = address.FullName;
        }
    }
}
