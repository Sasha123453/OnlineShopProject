using OnlineShop.Db.Models;

namespace OnlineShopProject.Models
{
    public class ProductPageViewModel
    {
        public List<ProductViewModel>  Products { get; set; }
        public bool IsInCart { get; set; }
        public bool IsInFavorite { get; set; }
        public bool IsInComparsion { get; set; }
    }
}
