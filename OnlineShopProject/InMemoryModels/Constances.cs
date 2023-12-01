using OnlineShopProject.Interfaces;

namespace OnlineShopProject.InMemoryModels
{
    public class Constances : IConstances
    {
        public string UserId { get; }
        private int productId;
        public int ProductId { get 
            {
                return productId++;
            }
        }
        public Constances()
        {
            UserId = "UserId";
            productId = 4;
        }
    }
}
