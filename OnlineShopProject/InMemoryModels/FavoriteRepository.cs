using OnlineShopProject.Interfaces;
using OnlineShopProject.Models;
using System;

namespace OnlineShopProject.InMemoryModels
{
    public class FavoriteRepository : IFavoriteRepository
    {
        List<UserWithProductsModel> Favorites = new List<UserWithProductsModel>();
        public UserWithProductsModel GetAllUserFavorites(string userId)
        {
            return Favorites.FirstOrDefault(x => x.UserId == userId);
        }
        public void AddToFavorites(ProductModel product, string userId)
        {
            var model = GetAllUserFavorites(userId);
            if (model == null)
            {
                model = new UserWithProductsModel
                {
                    UserId = userId,
                    Products = new List<ProductModel> { product }
                };
                Favorites.Add(model);
            }
            else
            {
                if (!model.Products.Contains(product)) model.Products.Add(product);
            }
        }
        public void Delete(ProductModel product, string userId)
        {
            var model = GetAllUserFavorites(userId);
            if (!(model == null))
            {
                model.Products.Remove(product);
            }
        }
    }
}
