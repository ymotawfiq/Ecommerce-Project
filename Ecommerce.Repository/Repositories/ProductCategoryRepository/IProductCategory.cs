
using Ecommerce.Data.Models.Entities;

namespace Ecommerce.Repository.Repositories.ProductCategoryRepository
{
    public interface IProductCategory
    {
        public ProductCategory AddNewCategory(ProductCategory productCategory);
        public ProductCategory UpdateCategory(ProductCategory productCategory);
        public ProductCategory DeleteCategory(Guid CategoryId);
        public ProductCategory GetCategoryById(Guid CategoryId);
        public IEnumerable<ProductCategory> GetAllCategories();
        public ProductCategory Upsert(ProductCategory productCategory);
        public void SaveChanges();
    }
}
