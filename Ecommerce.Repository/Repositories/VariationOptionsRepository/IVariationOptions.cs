

using Ecommerce.Data.Models.Entities;

namespace Ecommerce.Repository.Repositories.VariationOptionsRepository
{
    public interface IVariationOptions
    {
        public Task<VariationOptions> AddVariationOptionsAsync(VariationOptions variationOptions);
        public Task<VariationOptions> UpdateVariationOptionsAsync(VariationOptions variationOptions);
        public Task<VariationOptions> DeleteVariationOptionsByIdAsync(Guid id);
        public Task<VariationOptions> GetVariationOptionsByIdAsync(Guid id);
        public Task<IEnumerable<VariationOptions>> GetAllVariationOptionsAsync();
        public Task<IEnumerable<VariationOptions>> GetAllVariationOptionsByVariationIdAsync(Guid variationId);
        public Task SaveChangesAsync();
        public Task<VariationOptions> UpsertAsync(VariationOptions variationOptions);
    }
}
