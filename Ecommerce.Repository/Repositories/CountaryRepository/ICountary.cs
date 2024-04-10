using Ecommerce.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repository.Repositories.CountaryRepository
{
    public interface ICountary
    {
        public Task<Countary> AddCountaryAsync(Countary countary);
        public Task<Countary> UpdateCountaryAsync(Countary countary);
        public Task<Countary> DeleteCountaryByCountaryIdAsync(Guid countaryId);
        public Task<Countary> GetCountaryByCountaryIdAsync(Guid countaryId);
        public Task<IEnumerable<Countary>> GetAllCountariesAsync();
        public Task SaveChangesAsync();
        public Task<Countary> UpsertAsync(Countary countary);
    }
}
