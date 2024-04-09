

using Ecommerce.Data;
using Ecommerce.Data.Models.Entities;

namespace Ecommerce.Repository.Repositories.CountaryRepository
{
    public class CountaryRepository : ICountary
    {
        private readonly ApplicationDbContext _dbContext;
        public CountaryRepository(ApplicationDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        public Countary AddCountary(Countary countary)
        {
            try
            {
                _dbContext.Countary.Add(countary);
                SaveChanges();
                return countary;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Countary DeleteCountaryByCountaryId(Guid countaryId)
        {
            try
            {
                Countary countary = GetCountaryByCountaryId(countaryId);
                _dbContext.Countary.Remove(countary);
                SaveChanges();
                return countary;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Countary> GetAllCountaries()
        {
            try
            {
                return _dbContext.Countary.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Countary GetCountaryByCountaryId(Guid countaryId)
        {
            try
            {
                Countary? countary = _dbContext.Countary.Where(e => e.Id == countaryId).FirstOrDefault();
                if(countary == null)
                {
                    return null;
                }
                return countary;
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

        public Countary UpdateCountary(Countary countary)
        {
            try
            {
                Countary countary1 = GetCountaryByCountaryId(countary.Id);
                countary1.Name = countary.Name;
                SaveChanges();
                return countary1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Countary Upsert(Countary countary)
        {
            try
            {
                Countary countary1 = GetCountaryByCountaryId(countary.Id);
                if (countary1 == null)
                {
                    return AddCountary(countary);
                }
                return UpdateCountary(countary);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
