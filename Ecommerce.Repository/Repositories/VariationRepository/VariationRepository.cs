

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
        public async Task<Variation> AddVariationAsync(Variation variation)
        {
            try
            {
                await _dbContext.Variation.AddAsync(variation);
                SaveChangesAsync();
                return variation;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Variation> DeleteVariationByIdAsync(Guid variationId)
        {
            try
            {
                Variation variation = await GetVariationByIdAsync(variationId);
                _dbContext.Variation.Attach(variation);
                _dbContext.Variation.Remove(variation);
                SaveChangesAsync();
                return variation;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Variation>> GetAllVariationsAsync()
        {
            try
            {
                return await _dbContext.Variation.Include(e=>e.Category).Include(e=>e.VariationOptions)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Variation>> GetAllVariationsByCategoryIdAsync(Guid categoryId)
        {
            try
            {
                return
                    from v in await GetAllVariationsAsync()
                    where v.CategoryId == categoryId
                    select v;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Variation> GetVariationByIdAsync(Guid variationId)
        {
            try
            {
                Variation? variation = await _dbContext.Variation.Where(e => e.Id == variationId)
                    .FirstOrDefaultAsync();
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

        public void SaveChangesAsync()
        {
            _dbContext.SaveChangesAsync();
        }

        public async Task<Variation> UpdateVariationAsync(Variation variation)
        {
            try
            {
                Variation variation1 = await GetVariationByIdAsync(variation.Id);
                variation1.CategoryId = variation.CategoryId;
                variation1.Name = variation.Name;
                
                SaveChangesAsync();
                return variation1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Variation> UpsertAsync(Variation variation)
        {
            try
            {
                Variation oldVariation = await GetVariationByIdAsync(variation.Id);
                if(oldVariation == null)
                {
                    return await AddVariationAsync(variation);
                }
                return await UpdateVariationAsync(variation);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
