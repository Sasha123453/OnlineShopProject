namespace OnlineShopProject.Models
{
    public class UserAddressesModel
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public List<AddressModel> Addresses { get; set; }
        public int Amount { get
            {
                return Addresses.Count;
            } 
        }

    }
}
