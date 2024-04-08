

using Ecommerce.Data.Models.Entities;

namespace Ecommerce.Repository.Repositories.ProductItemRepository
{
    public interface IProductItem
    {
        public ProductItem AddProductItem(ProductItem productItem);
        public ProductItem UpdateProductItem(ProductItem productItem);
        public ProductItem DeleteProductItemById(Guid id);
        public ProductItem GetProductItemById(Guid id);
        public IEnumerable<ProductItem> GetAllProductItemsByProductId(Guid productId);
        public IEnumerable<ProductItem> GetAllItems();
        public void SaveChanges();
        public ProductItem Upsert(ProductItem productItem);
    }
}
