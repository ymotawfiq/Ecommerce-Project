

using Ecommerce.Data;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Repository.Repositories.ProductRepository;
using Ecommerce.Repository.Repositories.VariationRepository;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository.Repositories.ProductCategoryRepository
{
    public class ProductCategoryRepository : IProductCategory
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductCategoryRepository(ApplicationDbContext _dbContext
            )
        {
            this._dbContext = _dbContext;

        }
        public async Task<ProductCategory> AddNewCategoryAsync(ProductCategory productCategory)
        {
            try
            {
                await _dbContext.Category.AddAsync(productCategory);
                
                await SaveChangesAsync();
                return productCategory;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<ProductCategory> DeleteCategoryAsync(Guid CategoryId)
        {
            try
            {
                ProductCategory oldCategory = await GetCategoryByIdAsync(CategoryId);
                _dbContext.Category.Attach(oldCategory);
                _dbContext.Category.Remove(oldCategory);
                await SaveChangesAsync();
                return oldCategory;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ProductCategory> UpdateCategoryAsync(ProductCategory productCategory)
        {
            try
            {
                ProductCategory category = await GetCategoryByIdAsync(productCategory.Id);
                category.ParentCategoryId = productCategory.ParentCategoryId;
                category.CategoryName = productCategory.CategoryName;
                await SaveChangesAsync();
                return category;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ProductCategory>> GetAllCategoriesAsync()
        {
            try
            {
                return await _dbContext.Category.Include(e=>e.Variations).Include(e=>e.Products).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ProductCategory> GetCategoryByIdAsync(Guid CategoryId)
        {
            try
            {
                ProductCategory? category = await _dbContext.Category.Where(e => e.Id == CategoryId)
                    .FirstOrDefaultAsync(); 
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

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ProductCategory> UpsertAsync(ProductCategory productCategory)
        {
            try
            {
                ProductCategory? category = await GetCategoryByIdAsync(productCategory.Id);
                if (category == null)
                {
                    return await AddNewCategoryAsync(productCategory);
                }
                return await UpdateCategoryAsync(productCategory);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
