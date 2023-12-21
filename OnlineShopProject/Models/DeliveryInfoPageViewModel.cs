using OnlineShop.Db.Models;

namespace OnlineShopProject.Models
{
    public class DeliveryInfoPageViewModel
    {
        public Guid CurrentId { get; set; }
        public List<DeliveryInfoItemViewModel> Items { get; set; }
    }
}
