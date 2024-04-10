

using Ecommerce.Data;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Repository.Repositories.ProductRepository;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository.Repositories.ProductImagesRepository
{
    public class ProductImagesRepository : IProductImages
    {
        private readonly ApplicationDbContext _dbContext;
        public ProductImagesRepository(ApplicationDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        public async Task<ProductImages> AddProductImagesAsync(ProductImages productImages)
        {
            try
            {
                await _dbContext.ProductImages.AddAsync(productImages);
                await SaveChangesAsync();
                return productImages;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ProductImages> DeleteProductImagesAsync(Guid id)
        {
            try
            {
                ProductImages productImages = await GetProductImagesByIdAsync(id);
                _dbContext.ProductImages.Attach(productImages);
                _dbContext.ProductImages.Remove(productImages);
                await SaveChangesAsync();
                return productImages;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ProductImages>> GetAllProductsImagesAsync()
        {
            try
            {
                return await _dbContext.ProductImages.Include(p=>p.Product).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ProductImages> GetProductImagesByIdAsync(Guid id)
        {
            try
            {
                ProductImages? productImages = await _dbContext.ProductImages.Where(p => p.Id == id)
                    .FirstOrDefaultAsync();
                if(productImages == null)
                {
                    return null;
                }
                return productImages;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ProductImages>> GetProductImagesByProductIdAsync(Guid productId)
        {
            try
            {
                return
                    from p in await GetAllProductsImagesAsync()
                    where p.ProductId == productId
                    select p;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> RemoveImagesByProductIdAsync(Guid productId)
        {
            try
            {
                var images = _dbContext.ProductImages.Where(p => p.ProductId == productId);
                _dbContext.ProductImages.RemoveRange(images);
                await SaveChangesAsync();
                return true;
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

        public async Task<ProductImages> UpdateProductImagesAsync(ProductImages productImages)
        {
            try
            {
                ProductImages productImages1 = await GetProductImagesByIdAsync(productImages.Id);
                productImages1.ProductImageUrl = productImages.ProductImageUrl;
                await SaveChangesAsync();
                return productImages1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ProductImages> UpsertAsync(ProductImages productImages)
        {
            try
            {
                ProductImages productImages1 = await GetProductImagesByIdAsync(productImages.Id);
                if(productImages1 == null)
                {
                    return await AddProductImagesAsync(productImages);
                }
                return await UpdateProductImagesAsync(productImages);
            }
            catch (Exception)
            {
                throw;
            }
        }

        
    }
}
