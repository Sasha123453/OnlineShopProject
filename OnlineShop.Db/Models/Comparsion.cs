﻿

namespace OnlineShop.Db.Models
{
    public class Comparsion
    {
        public Guid Id { get; set; }
        public Product Product { get; set; }
        public string UserId { get; set; }
    }
}