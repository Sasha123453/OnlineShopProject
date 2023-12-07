namespace OnlineShopProject.Models
{
    public class DeliveryInfoViewModel
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public List<DeliveryInfoItemViewModel> DeliveryInfos { get; set; }
        public int Amount { get
            {
                return DeliveryInfos.Count;
            } 
        }

    }
}
