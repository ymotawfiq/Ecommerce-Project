

using Ecommerce.Data.Models.Entities;

namespace Ecommerce.Repository.Repositories.VariationRepository
{
    public interface IVariation
    {
        public Task<Variation> AddVariationAsync(Variation variation);
        public Task<Variation> UpdateVariationAsync(Variation variation);
        public Task<Variation> GetVariationByIdAsync(Guid variationId);
        public Task<Variation> DeleteVariationByIdAsync(Guid variationId);
        public Task<IEnumerable<Variation>> GetAllVariationsAsync();
        public Task<IEnumerable<Variation>> GetAllVariationsByCategoryIdAsync(Guid categoryId);
        public Task SaveChangesAsync();
        public Task<Variation> UpsertAsync(Variation variation);
    }
}
