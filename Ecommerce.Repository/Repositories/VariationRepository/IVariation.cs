

using Ecommerce.Data.Models.Entities;

namespace Ecommerce.Repository.Repositories.VariationRepository
{
    public interface IVariation
    {
        public Variation AddVariation(Variation variation);
        public Variation UpdateVariation(Variation variation);
        public Variation GetVariationById(Guid variationId);
        public Variation DeleteVariationById(Guid variationId);
        public IEnumerable<Variation> GetAllVariations();
        public IEnumerable<Variation> GetAllVariationsByCategoryId(Guid categoryId);
        public void SaveChanges();
        public Variation Upsert(Variation variation);
    }
}
