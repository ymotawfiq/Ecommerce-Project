

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
        public VariationOptions AddVariationOptions(VariationOptions variationOptions)
        {
            try
            {
                _dbContext.VariationOptions.Add(variationOptions);
      
                SaveChanges();
                return variationOptions;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public VariationOptions DeleteVariationOptionsById(Guid id)
        {
            try
            {
                VariationOptions variationOptions = GetVariationOptionsById(id);
                _dbContext.VariationOptions.Remove(variationOptions);

                SaveChanges();
                return variationOptions;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<VariationOptions> GetAllVariationOptions()
        {
            try
            {
                return _dbContext.VariationOptions.Include(e=>e.Variation)
                    .Include(e=>e.ProductVariation1).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<VariationOptions> GetAllVariationOptionsByVariationId(Guid variationId)
        {
            try
            {
                return
                    from v in GetAllVariationOptions()
                    where v.VariationId == variationId
                    select v;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public VariationOptions GetVariationOptionsById(Guid id)
        {
            try
            {
                VariationOptions? variationOptions = _dbContext.VariationOptions.Where(e => e.Id == id)
                    .FirstOrDefault();
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

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public VariationOptions UpdateVariationOptions(VariationOptions variationOptions)
        {
            try
            {
                VariationOptions variationOptions1 = GetVariationOptionsById(variationOptions.Id);
                variationOptions1.VariationId = variationOptions.VariationId;
                variationOptions1.Value = variationOptions.Value;

                SaveChanges();
                return variationOptions1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public VariationOptions Upsert(VariationOptions variationOptions)
        {
            try
            {
                VariationOptions variationOptions1 = GetVariationOptionsById(variationOptions.Id);
                if (variationOptions1 == null)
                {
                    return AddVariationOptions(variationOptions);
                }
                return UpdateVariationOptions(variationOptions);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
