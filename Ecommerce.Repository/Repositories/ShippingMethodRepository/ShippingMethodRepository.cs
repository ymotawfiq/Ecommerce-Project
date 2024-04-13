

using Ecommerce.Data;
using Ecommerce.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository.Repositories.ShippingMethodRepository
{
    public class ShippingMethodRepository : IShippingMethod
    {
        private readonly ApplicationDbContext _dbContext;
        public ShippingMethodRepository(ApplicationDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        public async Task<ShippingMethod> AddShippingMethodAsync(ShippingMethod shippingMethod)
        {
            try
            {
                await _dbContext.ShippingMethod.AddAsync(shippingMethod);
                await SaveChangesAsync();
                return shippingMethod;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ShippingMethod> DeleteShippingMethodByIdAsync(Guid shippingMethodId)
        {
            try
            {
                ShippingMethod shippingMethod1 = await GetShippingMethodByIdAsync(shippingMethodId);
                _dbContext.ShippingMethod.Remove(shippingMethod1);
                await SaveChangesAsync();
                return shippingMethod1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ShippingMethod>> GetAllShippingMethodAsync()
        {
            try
            {
                return await _dbContext.ShippingMethod.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ShippingMethod> GetShippingMethodByIdAsync(Guid shippingMethodId)
        {
            try
            {
                ShippingMethod? shippingMethod = await _dbContext.ShippingMethod
                    .Where(e => e.Id == shippingMethodId).FirstOrDefaultAsync();
                if (shippingMethod == null)
                {
                    return null;
                }
                return shippingMethod;
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

        public async Task<ShippingMethod> UpdateShippingMethodAsync(ShippingMethod shippingMethod)
        {
            try
            {
                ShippingMethod shippingMethod1 = await GetShippingMethodByIdAsync(shippingMethod.Id);
                shippingMethod1.Name = shippingMethod.Name;
                shippingMethod.Price = shippingMethod.Price;
                await SaveChangesAsync();
                return shippingMethod1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ShippingMethod> UpsertAsync(ShippingMethod shippingMethod)
        {
            try
            {
                ShippingMethod shippingMethod1 = await GetShippingMethodByIdAsync(shippingMethod.Id);
                if (shippingMethod1 == null)
                {
                    return await AddShippingMethodAsync(shippingMethod);
                }
                return await UpdateShippingMethodAsync(shippingMethod);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
