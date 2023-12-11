using AutoMapper;
using OnlineShop.Db.Models;
using OnlineShopProject.Models;

namespace OnlineShopProject.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Product, ProductViewModel>();
            CreateMap<ProductViewModel, Product>();
            CreateMap<Comparsion, ComparsionViewModel>();
            CreateMap<CartItem, CartItemViewModel>();
            CreateMap<Cart, CartViewModel>();
            CreateMap<DeliveryInfoItem, DeliveryInfoItemViewModel>();
            CreateMap<DeliveryInfo, DeliveryInfoViewModel>();
            CreateMap<DeliveryInfoViewModel, DeliveryInfo>();
            CreateMap<DeliveryInfoItemViewModel, DeliveryInfoItem>();
            CreateMap<Favorite, FavoriteViewModel>();
            CreateMap<Order, OrderViewModel>();
            CreateMap<OrderViewModel, Order>();
        }
    }
}
