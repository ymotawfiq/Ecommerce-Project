

using Ecommerce.Data;
using Ecommerce.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository.Repositories.PromotionCategoryRepository
{
    public class PromotionCategoryRepository : IPromotionCategory
    {
        private readonly ApplicationDbContext _dbContext;
        public PromotionCategoryRepository(ApplicationDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        public async Task<PromotionCategory> AddPromotionCategoryAsync(PromotionCategory promotionCategory)
        {
            try
            {
                await _dbContext.PromotionCategory.AddAsync(promotionCategory);
                await SaveChangesAsync();
                return promotionCategory;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PromotionCategory> DeletePromotionCategoryByIdAsync(Guid id)
        {
            try
            {
                PromotionCategory promotionCategory = await GetPromotionCategoryByIdAsync(id);
                _dbContext.PromotionCategory.Attach(promotionCategory);
                _dbContext.PromotionCategory.Remove(promotionCategory);
                await SaveChangesAsync();
                return promotionCategory;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<PromotionCategory>> GetAllPromotionsAsync()
        {
            try
            {
                return await _dbContext.PromotionCategory.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<PromotionCategory>> GetAllPromotionsByCategoryIdAsync(Guid categoryId)
        {
            try
            {
                return
                    from p in await GetAllPromotionsAsync()
                    where p.CategoryId == categoryId
                    select p;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<PromotionCategory>> GetAllPromotionsByPromotionIdAsync(Guid promotionId)
        {
            try
            {
                return
                    from p in await GetAllPromotionsAsync()
                    where p.PromotionId == promotionId
                    select p;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PromotionCategory> GetPromotionCategoryByIdAsync(Guid id)
        {
            try
            {
                PromotionCategory? promotionCategory = await _dbContext.PromotionCategory
                    .Where(e => e.Id == id).FirstOrDefaultAsync();
                return promotionCategory;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<PromotionCategory> UpdatePromotionCategoryAsync(PromotionCategory promotionCategory)
        {
            try
            {
                PromotionCategory promotionCategory1 = await GetPromotionCategoryByIdAsync(promotionCategory.Id);
                promotionCategory1.PromotionId = promotionCategory.PromotionId;
                promotionCategory1.CategoryId = promotionCategory.CategoryId;
                await SaveChangesAsync();
                return promotionCategory1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PromotionCategory> UpsertAsync(PromotionCategory promotionCategory)
        {
            try
            {
                PromotionCategory promotionCategory1 = await GetPromotionCategoryByIdAsync(promotionCategory.Id);
                if (promotionCategory1 == null)
                {
                    return await AddPromotionCategoryAsync(promotionCategory);
                }
                return await UpdatePromotionCategoryAsync(promotionCategory);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
