namespace OnlineShopProject.Models
{
    public class AddressModel
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public AddressModel(string address, string fullName, string phoneNumber)
        {
            Address = address;
            FullName = fullName;
            PhoneNumber = phoneNumber;
        }
        public AddressModel()
        {

        }
    }
}
