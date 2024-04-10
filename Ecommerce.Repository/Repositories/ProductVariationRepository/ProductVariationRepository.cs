

using Ecommerce.Data;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Repository.Repositories.ProductItemRepository;
using Ecommerce.Repository.Repositories.VariationOptionsRepository;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository.Repositories.ProductVariationRepository
{
    public class ProductVariationRepository : IProductVariation
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductVariationRepository(ApplicationDbContext _dbContext
            )
        {
            this._dbContext = _dbContext;

        }

        public async Task<ProductVariation> AddProductVariationAsync(ProductVariation productVariation)
        {
            try
            {
                await _dbContext.ProductVariation.AddAsync(productVariation);
                await SaveChangesAsync();
                return productVariation;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ProductVariation> DeleteProductVariationByIdAsync(Guid id)
        {
            try
            {
                ProductVariation productVariation = await GetProductVariationByIdAsync(id);
                _dbContext.ProductVariation.Attach(productVariation);
                _dbContext.ProductVariation.Remove(productVariation);
                await SaveChangesAsync();
                return productVariation;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ProductVariation>> GetAllProductVariationsAsync()
        {
            try
            {
                return await _dbContext.ProductVariation.Include(e=>e.ProductItem)
                    .Include(e=>e.VariationOption).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ProductVariation>> GetAllVariationsByProductItemIdAsync(Guid productItemId)
        {
            try
            {
                return
                    from v in await GetAllProductVariationsAsync()
                    where v.ProductItemId == productItemId
                    select v;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ProductVariation>> GetAllVariationsByVariationOptionIdAsync
            (Guid variationOptionId)
        {
            try
            {
                return
                    from v in await GetAllProductVariationsAsync()
                    where v.VariationOptionId == variationOptionId
                    select v;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ProductVariation> GetProductVariationByIdAsync(Guid id)
        {
            try
            {
                ProductVariation? productVariation = await _dbContext.ProductVariation
                    .Where(e => e.Id == id).FirstOrDefaultAsync();
                if(productVariation == null)
                {
                    return null;
                }
                return productVariation;
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

        public async Task<ProductVariation> UpdateProductVariationAsync(ProductVariation productVariation)
        {
            try
            {
                ProductVariation productVariation1 = await GetProductVariationByIdAsync(productVariation.Id);
                productVariation1.ProductItemId = productVariation.ProductItemId;
                productVariation1.VariationOptionId = productVariation.VariationOptionId;
                await SaveChangesAsync();
                return productVariation1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ProductVariation> UpsertAsync(ProductVariation productVariation)
        {
            try
            {
                ProductVariation productVariation1 = await GetProductVariationByIdAsync(productVariation.Id);
                if(productVariation1 == null)
                {
                    return await AddProductVariationAsync(productVariation);
                }
                return await UpdateProductVariationAsync(productVariation);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
