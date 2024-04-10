

using Ecommerce.Data;
using Ecommerce.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository.Repositories.PromotionRepository
{
    public class PromotionRepository : IPromotion
    {
        private readonly ApplicationDbContext _dbContext;
        public PromotionRepository(ApplicationDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        public async Task<Promotion> AddPromotionAsync(Promotion promotion)
        {
            try
            {
                await _dbContext.Promotion.AddAsync(promotion);
                await SaveChangesAsync();
                return promotion;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Promotion> DeletePromotionByIdAsync(Guid id)
        {
            try
            {
                Promotion promotion = await GetPromotionByIdAsync(id);
                _dbContext.Promotion.Attach(promotion);
                _dbContext.Promotion.Remove(promotion);
                await SaveChangesAsync();
                return promotion;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Promotion>> GetAllPromotionsAsync()
        {
            try
            {
                return await _dbContext.Promotion.Include(e=>e.PromotionCategories).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<Promotion> GetPromotionByIdAsync(Guid id)
        {
            try
            {
                Promotion? promotion = await _dbContext.Promotion.Where(e => e.Id == id)
                    .FirstOrDefaultAsync();
                if (promotion == null)
                {
                    return null;
                }
                return promotion;
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

        public async Task<Promotion> UpdatePromotionAsync(Promotion promotion)
        {
            try
            {
                Promotion promotion1 = await GetPromotionByIdAsync(promotion.Id);
                promotion1.Name = promotion.Name;
                promotion1.StartDate = promotion.StartDate;
                promotion1.EndDate = promotion.EndDate;
                promotion1.Description = promotion.Description;
                await SaveChangesAsync();
                return promotion1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Promotion> UpsertAsync(Promotion promotion)
        {
            try
            {
                Promotion promotion1 = await GetPromotionByIdAsync(promotion.Id);
                if(promotion1 == null)
                {
                    return await AddPromotionAsync(promotion);
                }
                return await UpdatePromotionAsync(promotion);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
