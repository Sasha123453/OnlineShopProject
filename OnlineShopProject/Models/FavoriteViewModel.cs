﻿namespace OnlineShopProject.Models
{
    public class FavoriteViewModel
    {
        public Guid Id { get; set; }
        public ProductViewModel Product { get; set; }
        public string UserId { get; set; }
    }
}