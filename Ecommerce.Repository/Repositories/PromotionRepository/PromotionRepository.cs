

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
        public Promotion AddPromotion(Promotion promotion)
        {
            try
            {
                _dbContext.Promotion.Add(promotion);
                SaveChanges();
                return promotion;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Promotion DeletePromotionById(Guid id)
        {
            try
            {
                Promotion promotion = GetPromotionById(id);
                _dbContext.Promotion.Remove(promotion);
                SaveChanges();
                return promotion;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Promotion> GetAllPromotions()
        {
            try
            {
                return _dbContext.Promotion.Include(e=>e.PromotionCategories).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public Promotion GetPromotionById(Guid id)
        {
            try
            {
                Promotion? promotion = _dbContext.Promotion.Where(e => e.Id == id).FirstOrDefault();
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

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public Promotion UpdatePromotion(Promotion promotion)
        {
            try
            {
                Promotion promotion1 = GetPromotionById(promotion.Id);
                promotion1.Name = promotion.Name;
                promotion1.StartDate = promotion.StartDate;
                promotion1.EndDate = promotion.EndDate;
                promotion1.Description = promotion.Description;
                SaveChanges();
                return promotion1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Promotion Upsert(Promotion promotion)
        {
            try
            {
                Promotion promotion1 = GetPromotionById(promotion.Id);
                if(promotion1 == null)
                {
                    return AddPromotion(promotion);
                }
                return UpdatePromotion(promotion);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
