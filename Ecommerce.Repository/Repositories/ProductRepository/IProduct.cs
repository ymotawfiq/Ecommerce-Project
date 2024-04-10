

using Ecommerce.Data.Models.Entities;

namespace Ecommerce.Repository.Repositories.ProductRepository
{
    public interface IProduct
    {
        public Task<Product> AddProductAsync(Product product);
        public Task<Product> UpdateProductAsync(Product product);
        public Task<Product> GetProductByIdAsync(Guid productId);
        public Task<Product> DeleteProductByIdAsync(Guid productId);
        public Task<IEnumerable<Product>> GetAllProductsAsync();
        public Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(Guid CategoryId);
        public Task<Product> UpsertAsync(Product product);
        public Task SaveChangesAsync();
    }
}
