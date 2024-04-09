using Ecommerce.Data.Models.Entities;


namespace Ecommerce.Repository.Repositories.PromotionCategoryRepository
{
    public interface IPromotionCategory
    {
        public PromotionCategory AddPromotionCategory(PromotionCategory promotionCategory);
        public PromotionCategory UpdatePromotionCategory(PromotionCategory promotionCategory);
        public PromotionCategory GetPromotionCategoryById(Guid id);
        public PromotionCategory DeletePromotionCategoryById(Guid id);
        public IEnumerable<PromotionCategory> GetAllPromotions();
        public IEnumerable<PromotionCategory> GetAllPromotionsByPromotionId(Guid promotionId);
        public IEnumerable<PromotionCategory> GetAllPromotionsByCategoryId(Guid categoryId);
        public void SaveChanges();
        public PromotionCategory Upsert(PromotionCategory promotionCategory);
    }
}
