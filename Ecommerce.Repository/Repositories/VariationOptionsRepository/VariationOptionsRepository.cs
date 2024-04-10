

using Ecommerce.Data;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Repository.Repositories.ProductVariationRepository;
using Ecommerce.Repository.Repositories.VariationRepository;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository.Repositories.VariationOptionsRepository
{
    public class VariationOptionsRepository : IVariationOptions
    {
        private readonly ApplicationDbContext _dbContext;
        public VariationOptionsRepository(ApplicationDbContext _dbContext
            )
        {
            this._dbContext = _dbContext;
        }
        public async Task<VariationOptions> AddVariationOptionsAsync(VariationOptions variationOptions)
        {
            try
            {
                await _dbContext.VariationOptions.AddAsync(variationOptions);
      
                await SaveChangesAsync();
                return variationOptions;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<VariationOptions> DeleteVariationOptionsByIdAsync(Guid id)
        {
            try
            {
                VariationOptions variationOptions = await GetVariationOptionsByIdAsync(id);
                _dbContext.VariationOptions.Attach(variationOptions);
                _dbContext.VariationOptions.Remove(variationOptions);

                await SaveChangesAsync();
                return variationOptions;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<VariationOptions>> GetAllVariationOptionsAsync()
        {
            try
            {
                return await _dbContext.VariationOptions.Include(e=>e.Variation)
                    .Include(e=>e.ProductVariation1).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<VariationOptions>> GetAllVariationOptionsByVariationIdAsync
            (Guid variationId)
        {
            try
            {
                return
                    from v in await GetAllVariationOptionsAsync()
                    where v.VariationId == variationId
                    select v;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<VariationOptions> GetVariationOptionsByIdAsync(Guid id)
        {
            try
            {
                VariationOptions? variationOptions = await _dbContext.VariationOptions.Where(e => e.Id == id)
                    .FirstOrDefaultAsync();
                if (variationOptions == null)
                {
                    return null;
                }

                return variationOptions;
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

        public async Task<VariationOptions> UpdateVariationOptionsAsync(VariationOptions variationOptions)
        {
            try
            {
                VariationOptions variationOptions1 = await GetVariationOptionsByIdAsync(variationOptions.Id);
                variationOptions1.VariationId = variationOptions.VariationId;
                variationOptions1.Value = variationOptions.Value;

                await SaveChangesAsync();
                return variationOptions1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<VariationOptions> UpsertAsync(VariationOptions variationOptions)
        {
            try
            {
                VariationOptions variationOptions1 = await GetVariationOptionsByIdAsync(variationOptions.Id);
                if (variationOptions1 == null)
                {
                    return await AddVariationOptionsAsync(variationOptions);
                }
                return await UpdateVariationOptionsAsync(variationOptions);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
