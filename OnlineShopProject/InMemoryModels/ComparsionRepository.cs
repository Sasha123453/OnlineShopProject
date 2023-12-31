﻿using OnlineShopProject.Interfaces;
using OnlineShopProject.Models;

namespace OnlineShopProject.InMemoryModels
{
    public class ComparsionRepository : IComparsionRepository
    {
        public List<UserWithProductsModel> Comparsions = new List<UserWithProductsModel>();
        public UserWithProductsModel GetAllUserComparsions(string userId)
        {
            return Comparsions.FirstOrDefault(x => x.UserId == userId);
        }
        public void AddToComparsion(ProductModel product, string userId)
        {
            var model = GetAllUserComparsions(userId);
            if (model == null)
            {
                model = new UserWithProductsModel
                {
                    UserId = userId,
                    Products = new List<ProductModel> { product }
                };
                Comparsions.Add(model);
            }
            else
            {
                if (!model.Products.Contains(product)) model.Products.Add(product);
            }
        }
        public void Delete(ProductModel product, string userId)
        {
            var model = GetAllUserComparsions(userId);
            if (!(model == null))
            {
                model.Products.Remove(product);
            }
        }
    }
}
