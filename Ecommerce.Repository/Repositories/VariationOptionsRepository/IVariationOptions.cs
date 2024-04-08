

using Ecommerce.Data.Models.Entities;

namespace Ecommerce.Repository.Repositories.VariationOptionsRepository
{
    public interface IVariationOptions
    {
        public VariationOptions AddVariationOptions(VariationOptions variationOptions);
        public VariationOptions UpdateVariationOptions(VariationOptions variationOptions);
        public VariationOptions DeleteVariationOptionsById(Guid id);
        public VariationOptions GetVariationOptionsById(Guid id);
        public IEnumerable<VariationOptions> GetAllVariationOptions();
        public IEnumerable<VariationOptions> GetAllVariationOptionsByVariationId(Guid variationId);
        public void SaveChanges();
        public VariationOptions Upsert(VariationOptions variationOptions);
    }
}
