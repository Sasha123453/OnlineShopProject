using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Db.Models
{
    public class Cart
    {
        public Guid Id { get; set; }
        public List<CartItem> Items { get; set; }
        public string UserId { get; set; }
    }
}
