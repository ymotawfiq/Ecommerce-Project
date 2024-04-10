
using Ecommerce.Data.Models.Entities;

namespace Ecommerce.Repository.Repositories.PromotionRepository
{
    public interface IPromotion
    {
        public Task<Promotion> AddPromotionAsync(Promotion promotion);
        public Task<Promotion> UpdatePromotionAsync(Promotion promotion);
        public Task<Promotion> GetPromotionByIdAsync(Guid id);
        public Task<Promotion> DeletePromotionByIdAsync(Guid id);
        public Task<IEnumerable<Promotion>> GetAllPromotionsAsync();
        public void SaveChangesAsync();
        public Task<Promotion> UpsertAsync(Promotion promotion);
    }
}
