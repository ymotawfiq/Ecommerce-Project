using Ecommerce.Data;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Repository.Repositories.ProductCategoryRepository;
using Ecommerce.Repository.Repositories.ProductImagesRepository;
using Ecommerce.Repository.Repositories.ProductItemRepository;
using Microsoft.EntityFrameworkCore;


namespace Ecommerce.Repository.Repositories.ProductRepository
{
    public class ProductRepository : IProduct
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext _dbContext
            )
        {
            this._dbContext = _dbContext;
        }
        public async Task<Product> AddProductAsync(Product product)
        {
            try
            {
                await _dbContext.Product.AddAsync(product);
                await SaveChangesAsync();
                return product;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Product> DeleteProductByIdAsync(Guid productId)
        {
            try
            {
                Product product = await GetProductByIdAsync(productId);
                //_dbContext.Product.Attach(product);
                _dbContext.Product.Remove(product);
                await SaveChangesAsync();
                return product;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            try
            {
                return await _dbContext.Product.Include(e=>e.Category).Include(e=>e.ProductImages)
                    .Include(e=>e.ProductItems).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Product> GetProductByIdAsync(Guid productId)
        {
            try
            {
                Product? product = await _dbContext.Product.Where(p => p.Id == productId)
                    .FirstOrDefaultAsync();
                if(product == null)
                {
                    return null;
                }
                return product;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(Guid CategoryId)
        {
            try
            {
                return (
                    from p in await GetAllProductsAsync()
                    where p.CategoryId == CategoryId
                    select p
                    );
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

        public async Task<Product> UpdateProductAsync(Product product)
        {
            try
            {
                Product? product1 = await GetProductByIdAsync(product.Id);
                product1.Name = product.Name;
                product1.CategoryId = product.CategoryId;
                product1.Description = product.Description;
                await SaveChangesAsync();
                return product1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Product> UpsertAsync(Product product)
        {
            try
            {
                Product? product1 = await GetProductByIdAsync(product.Id);
                if (product1 == null)
                {
                    return await AddProductAsync(product);
                }
                return await UpdateProductAsync(product);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
