using Ecommerce.Data.Models.Entities;


namespace Ecommerce.Repository.Repositories.PromotionCategoryRepository
{
    public interface IPromotionCategory
    {
        public Task<PromotionCategory> AddPromotionCategoryAsync(PromotionCategory promotionCategory);
        public Task<PromotionCategory> UpdatePromotionCategoryAsync(PromotionCategory promotionCategory);
        public Task<PromotionCategory> GetPromotionCategoryByIdAsync(Guid id);
        public Task<PromotionCategory> DeletePromotionCategoryByIdAsync(Guid id);
        public Task<IEnumerable<PromotionCategory>> GetAllPromotionsAsync();
        public Task<IEnumerable<PromotionCategory>> GetAllPromotionsByPromotionIdAsync(Guid promotionId);
        public Task<IEnumerable<PromotionCategory>> GetAllPromotionsByCategoryIdAsync(Guid categoryId);
        public void SaveChangesAsync();
        public Task<PromotionCategory> UpsertAsync(PromotionCategory promotionCategory);
    }
}
