

using Ecommerce.Data.Models.Entities;

namespace Ecommerce.Repository.Repositories.ProductRepository
{
    public interface IProduct
    {
        public Product AddProduct(Product product);
        public Product UpdateProduct(Product product);
        public Product GetProductById(Guid productId);
        public Product DeleteProductById(Guid productId);
        public IEnumerable<Product> GetAllProducts();
        public IEnumerable<Product> GetProductsByCategoryId(Guid CategoryId);
        public Product Upsert(Product product);
        public void SaveChanges();
    }
}
