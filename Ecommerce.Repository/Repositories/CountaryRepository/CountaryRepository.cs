

using Ecommerce.Data;
using Ecommerce.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository.Repositories.CountaryRepository
{
    public class CountaryRepository : ICountary
    {
        private readonly ApplicationDbContext _dbContext;
        public CountaryRepository(ApplicationDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        public async Task<Countary> AddCountaryAsync(Countary countary)
        {
            try
            {
                await _dbContext.Countary.AddAsync(countary);
                await SaveChangesAsync();
                return countary;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Countary> DeleteCountaryByCountaryIdAsync(Guid countaryId)
        {
            try
            {
                Countary countary = await GetCountaryByCountaryIdAsync(countaryId);
                _dbContext.Countary.Attach(countary);
                _dbContext.Countary.Remove(countary);
                await SaveChangesAsync();
                return countary;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Countary>> GetAllCountariesAsync()
        {
            try
            {
                return await _dbContext.Countary.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Countary> GetCountaryByCountaryIdAsync(Guid countaryId)
        {
            try
            {
                Countary? countary = await _dbContext.Countary.Where(e => e.Id == countaryId)
                    .FirstOrDefaultAsync();
                return countary;
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

        public async Task<Countary> UpdateCountaryAsync(Countary countary)
        {
            try
            {
                Countary countary1 = await GetCountaryByCountaryIdAsync(countary.Id);
                countary1.Name = countary.Name;
                await SaveChangesAsync();
                return countary1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Countary> UpsertAsync(Countary countary)
        {
            try
            {
                Countary countary1 = await GetCountaryByCountaryIdAsync(countary.Id);
                if (countary1 == null)
                {
                    return await AddCountaryAsync(countary);
                }
                return await UpdateCountaryAsync(countary);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
