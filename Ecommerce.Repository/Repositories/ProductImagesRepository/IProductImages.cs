

using Ecommerce.Data.Models.Entities;

namespace Ecommerce.Repository.Repositories.ProductImagesRepository
{
    public interface IProductImages
    {
        public Task<ProductImages> AddProductImagesAsync(ProductImages productImages);
        public Task<ProductImages> UpdateProductImagesAsync(ProductImages productImages);
        public Task<IEnumerable<ProductImages>> GetProductImagesByProductIdAsync(Guid productId);
        public Task<ProductImages> DeleteProductImagesAsync(Guid id);
        public Task<ProductImages> GetProductImagesByIdAsync(Guid id);
        public Task<IEnumerable<ProductImages>> GetAllProductsImagesAsync();
        public void SaveChangesAsync();
        public Task<ProductImages> UpsertAsync(ProductImages productImages);
        public bool RemoveImagesByProductIdAsync(Guid productId);
    }
}
