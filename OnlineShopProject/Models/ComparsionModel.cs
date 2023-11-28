namespace OnlineShopProject.Models
{
    public class UserWithProductsModel
    {
        public List<ProductModel> Products { get; set; }
        public string UserId { get; set; }
    }
}
