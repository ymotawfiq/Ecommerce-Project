
using Ecommerce.Data.EntityConfigurations;
using Ecommerce.Data.Models.Entities;

namespace Ecommerce.Repository.Repositories.ProductVariationRepository
{
    public interface IProductVariation
    {
        public ProductVariation AddProductVariation(ProductVariation productVariation);
        public ProductVariation UpdateProductVariation(ProductVariation productVariation);
        public ProductVariation DeleteProductVariationById(Guid id);
        public ProductVariation GetProductVariationById(Guid id);
        public IEnumerable<ProductVariation> GetAllProductVariations();
        public IEnumerable<ProductVariation> GetAllVariationsByProductItemId(Guid productItemId);
        public IEnumerable<ProductVariation> GetAllVariationsByVariationOptionId(Guid variationOptionId);
        public void SaveChanges();
        public ProductVariation Upsert(ProductVariation productVariation);

    }
}
