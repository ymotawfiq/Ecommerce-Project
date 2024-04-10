
using Ecommerce.Data.EntityConfigurations;
using Ecommerce.Data.Models.Entities;

namespace Ecommerce.Repository.Repositories.ProductVariationRepository
{
    public interface IProductVariation
    {
        public Task<ProductVariation> AddProductVariationAsync(ProductVariation productVariation);
        public Task<ProductVariation> UpdateProductVariationAsync(ProductVariation productVariation);
        public Task<ProductVariation> DeleteProductVariationByIdAsync(Guid id);
        public Task<ProductVariation> GetProductVariationByIdAsync(Guid id);
        public Task<IEnumerable<ProductVariation>> GetAllProductVariationsAsync();
        public Task<IEnumerable<ProductVariation>> GetAllVariationsByProductItemIdAsync(Guid productItemId);
        public Task<IEnumerable<ProductVariation>> GetAllVariationsByVariationOptionIdAsync(Guid variationOptionId);
        public Task SaveChangesAsync();
        public Task<ProductVariation> UpsertAsync(ProductVariation productVariation);

    }
}
