

using Ecommerce.Data.Models.Entities;

namespace Ecommerce.Repository.Repositories.ProductImagesRepository
{
    public interface IProductImages
    {
        public ProductImages AddProductImages(ProductImages productImages);
        public ProductImages UpdateProductImages(ProductImages productImages);
        public IEnumerable<ProductImages> GetProductImagesByProductId(Guid productId);
        public ProductImages DeleteProductImages(Guid id);
        public ProductImages GetProductImagesById(Guid id);
        public IEnumerable<ProductImages> GetAllProductsImages();
        public void SaveChanges();
        public ProductImages Upsert(ProductImages productImages);
        public bool RemoveImagesByProductId(Guid productId);
    }
}
