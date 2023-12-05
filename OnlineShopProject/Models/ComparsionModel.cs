namespace OnlineShopProject.Models
{
    public class UserWithProductsModel
    {
        public List<ProductViewModel> Products { get; set; }
        public string UserId { get; set; }
    }
}
