using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShopProject.Interfaces;
using OnlineShopProject.Models;
using System;
using System.Collections.Concurrent;
using System.Data;

namespace OnlineShopProject.InMemoryModels
{
    public class UserOrders
    {
        private ICartsRepository _cartsRepository;
        private ConcurrentDictionary<string, (DateTime, List<CartItem>)> userOrders;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        PeriodicTimer _timer;
        public UserOrders(ICartsRepository cartsRepository, IServiceScopeFactory serviceScopeFactory)
        {
            userOrders = new ConcurrentDictionary<string, (DateTime, List<CartItem>)>();
            _cartsRepository = cartsRepository;
            _timer = new PeriodicTimer(TimeSpan.FromMinutes(30));
            _serviceScopeFactory = serviceScopeFactory;
            CleanOrders(); 
        }
        public List<CartItem> GetOrdersById(string userId)
        {
            if (userOrders.ContainsKey(userId)) { return userOrders[userId].Item2; }
            throw new Exception();
        }
        //public void AddOrDeletePreorders(int id, string userId)
        //{
        //    if (!userOrders.ContainsKey(userId))
        //    {
        //        userOrders.TryAdd(userId, (DateTime.Now, new List<CartItem>()));
        //    }
        //    var orders = userOrders[userId].Item2;
        //    var cartItem = _cartsRepository.GetCartByUserId(userId).Items.Where(x => x.Product.Id == id).FirstOrDefault();
        //    if (orders.Contains(cartItem))
        //    {
        //        orders.Remove(cartItem);
        //    }
        //    else
        //    {
        //        orders.Add(cartItem);
        //    }


        //}
        public void Update(string userId)
        {
            if (!userOrders.ContainsKey(userId)) return;
            var x = userOrders[userId];
            x.Item1 = DateTime.Now;
        }
        private async void CleanOrders()
        {
            while (await _timer.WaitForNextTickAsync())
            {
                foreach (var order in userOrders)
                {
                    if (DateTime.Now - order.Value.Item1 > TimeSpan.FromMinutes(30))
                    {
                        userOrders.TryRemove(order.Key, out var _);
                    }
                }
            }
        }
    }
}
