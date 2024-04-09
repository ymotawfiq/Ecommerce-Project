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
        public Countary AddCountary(Countary countary);
        public Countary UpdateCountary(Countary countary);
        public Countary DeleteCountaryByCountaryId(Guid countaryId);
        public Countary GetCountaryByCountaryId(Guid countaryId);
        public IEnumerable<Countary> GetAllCountaries();
        public void SaveChanges();
        public Countary Upsert(Countary countary);
    }
}
