

using Ecommerce.Data;
using Ecommerce.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository.Repositories.ProductCategoryRepository
{
    public class ProductCategoryRepository : IProductCategory
    {
        private readonly ApplicationDbContext _dbContext;
        public ProductCategoryRepository(ApplicationDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        public ProductCategory AddNewCategory(ProductCategory productCategory)
        {
            try
            {
                _dbContext.Category.Add(productCategory);
                SaveChanges();
                return productCategory;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public ProductCategory DeleteCategory(Guid CategoryId)
        {
            try
            {
                ProductCategory category = GetCategoryById(CategoryId);
                _dbContext.Category.Remove(category);
                SaveChanges();
                return category;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ProductCategory UpdateCategory(ProductCategory productCategory)
        {
            try
            {
                ProductCategory category = GetCategoryById(productCategory.Id);
                category.ParentCategoryId = productCategory.ParentCategoryId;
                category.CategoryName = productCategory.CategoryName;
                SaveChanges();
                return category;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<ProductCategory> GetAllCategories()
        {
            try
            {
                return _dbContext.Category.Include(e=>e.Variations).Include(e=>e.Products).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ProductCategory GetCategoryById(Guid CategoryId)
        {
            try
            {
                ProductCategory? category = _dbContext.Category.Where(e => e.Id == CategoryId).FirstOrDefault(); 
                if(category == null)
                {
                    return null;
                }
                return category;
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

        public ProductCategory Upsert(ProductCategory productCategory)
        {
            try
            {
                ProductCategory? category = GetCategoryById(productCategory.Id);
                if (category == null)
                {
                    return AddNewCategory(productCategory);
                }
                return UpdateCategory(productCategory);
            }
            catch (Exception)
            {
                throw;
            }
        }

       
    }
}
