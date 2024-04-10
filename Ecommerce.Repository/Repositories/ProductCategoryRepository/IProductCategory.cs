
using Ecommerce.Data.Models.Entities;

namespace Ecommerce.Repository.Repositories.ProductCategoryRepository
{
    public interface IProductCategory
    {
        public Task<ProductCategory> AddNewCategoryAsync(ProductCategory productCategory);
        public Task<ProductCategory> UpdateCategoryAsync(ProductCategory productCategory);
        public Task<ProductCategory> DeleteCategoryAsync(Guid CategoryId);
        public Task<ProductCategory> GetCategoryByIdAsync(Guid CategoryId);
        public Task<IEnumerable<ProductCategory>> GetAllCategoriesAsync();
        public Task<ProductCategory> UpsertAsync(ProductCategory productCategory);
        public void SaveChangesAsync();
    }
}
