

using Ecommerce.Data.Models.Entities;

namespace Ecommerce.Repository.Repositories.ProductItemRepository
{
    public interface IProductItem
    {
        public Task<ProductItem> AddProductItemAsync(ProductItem productItem);
        public Task<ProductItem> UpdateProductItemAsync(ProductItem productItem);
        public Task<ProductItem> DeleteProductItemByIdAsync(Guid id);
        public Task<ProductItem> GetProductItemByIdAsync(Guid id);
        public Task<IEnumerable<ProductItem>> GetAllProductItemsByProductIdAsync(Guid productId);
        public Task<IEnumerable<ProductItem>> GetAllItemsAsync();
        public Task SaveChangesAsync();
        public Task<ProductItem> UpsertAsync(ProductItem productItem);
    }
}
