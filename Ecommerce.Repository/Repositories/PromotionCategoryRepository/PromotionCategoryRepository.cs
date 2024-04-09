

using Ecommerce.Data;
using Ecommerce.Data.Models.Entities;

namespace Ecommerce.Repository.Repositories.PromotionCategoryRepository
{
    public class PromotionCategoryRepository : IPromotionCategory
    {
        private readonly ApplicationDbContext _dbContext;
        public PromotionCategoryRepository(ApplicationDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        public PromotionCategory AddPromotionCategory(PromotionCategory promotionCategory)
        {
            try
            {
                _dbContext.PromotionCategory.Add(promotionCategory);
                SaveChanges();
                return promotionCategory;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public PromotionCategory DeletePromotionCategoryById(Guid id)
        {
            try
            {
                PromotionCategory promotionCategory = GetPromotionCategoryById(id);
                _dbContext.PromotionCategory.Remove(promotionCategory);
                SaveChanges();
                return promotionCategory;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<PromotionCategory> GetAllPromotions()
        {
            try
            {
                return _dbContext.PromotionCategory.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<PromotionCategory> GetAllPromotionsByCategoryId(Guid categoryId)
        {
            try
            {
                return
                    from p in GetAllPromotions()
                    where p.CategoryId == categoryId
                    select p;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<PromotionCategory> GetAllPromotionsByPromotionId(Guid promotionId)
        {
            try
            {
                return
                    from p in GetAllPromotions()
                    where p.PromotionId == promotionId
                    select p;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public PromotionCategory GetPromotionCategoryById(Guid id)
        {
            try
            {
                PromotionCategory? promotionCategory = _dbContext.PromotionCategory
                    .Where(e => e.Id == id).FirstOrDefault();
                if(promotionCategory == null)
                {
                    return null;
                }
                return promotionCategory;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public PromotionCategory UpdatePromotionCategory(PromotionCategory promotionCategory)
        {
            try
            {
                PromotionCategory promotionCategory1 = GetPromotionCategoryById(promotionCategory.Id);
                promotionCategory1.PromotionId = promotionCategory.PromotionId;
                promotionCategory1.CategoryId = promotionCategory.CategoryId;
                SaveChanges();
                return promotionCategory1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public PromotionCategory Upsert(PromotionCategory promotionCategory)
        {
            try
            {
                PromotionCategory promotionCategory1 = GetPromotionCategoryById(promotionCategory.Id);
                if (promotionCategory1 == null)
                {
                    return AddPromotionCategory(promotionCategory);
                }
                return UpdatePromotionCategory(promotionCategory);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
