namespace OnlineShopProject.Models
{
    public class ComparsionRepository : IComparsionRepository
    {
        public List<ComparsionModel> Comparsions = new List<ComparsionModel>();
        public ComparsionModel GetAllUserComparsions(string userId)
        {
            return Comparsions.FirstOrDefault(x => x.UserId == userId);
        }
        public void AddToComparsion(ProductModel product, string userId)
        {
            var model = GetAllUserComparsions(userId);
            if (model == null)
            {
                model = new ComparsionModel
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
    }
}
