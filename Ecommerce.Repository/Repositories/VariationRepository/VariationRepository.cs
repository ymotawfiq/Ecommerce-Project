

using Ecommerce.Data;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Repository.Repositories.ProductCategoryRepository;
using Ecommerce.Repository.Repositories.VariationOptionsRepository;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository.Repositories.VariationRepository
{
    public class VariationRepository : IVariation
    {
        private readonly ApplicationDbContext _dbContext;

        public VariationRepository(ApplicationDbContext _dbContext)
        {
            this._dbContext = _dbContext;

        }
        public Variation AddVariation(Variation variation)
        {
            try
            {
                _dbContext.Variation.Add(variation);
                SaveChanges();
                return variation;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Variation DeleteVariationById(Guid variationId)
        {
            try
            {
                Variation variation = GetVariationById(variationId);
              
                _dbContext.Variation.Remove(variation);
                SaveChanges();
                return variation;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Variation> GetAllVariations()
        {
            try
            {
                return _dbContext.Variation.Include(e=>e.Category).Include(e=>e.VariationOptions).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Variation> GetAllVariationsByCategoryId(Guid categoryId)
        {
            try
            {
                return
                    from v in GetAllVariations()
                    where v.CategoryId == categoryId
                    select v;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Variation GetVariationById(Guid variationId)
        {
            try
            {
                Variation? variation = _dbContext.Variation.Where(e => e.Id == variationId).FirstOrDefault();
                if (variation == null)
                {
                    return null;
                }
                
                return variation;
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

        public Variation UpdateVariation(Variation variation)
        {
            try
            {
                Variation variation1 = GetVariationById(variation.Id);
                variation1.CategoryId = variation.CategoryId;
                variation1.Name = variation.Name;
                
                SaveChanges();
                return variation1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Variation Upsert(Variation variation)
        {
            try
            {
                Variation oldVariation = GetVariationById(variation.Id);
                if(oldVariation == null)
                {
                    return AddVariation(variation);
                }
                return UpdateVariation(variation);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
