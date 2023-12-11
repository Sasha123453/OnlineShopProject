namespace OnlineShopProject.Models
{
    public class DeliveryInfoViewModel
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public List<DeliveryInfoItemViewModel> DeliveryInfoItems { get; set; }
        public int Amount { get
            {
                return DeliveryInfoItems.Count;
            } 
        }

    }
}
