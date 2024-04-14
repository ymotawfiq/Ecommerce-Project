

using Ecommerce.Data;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Repository.Repositories.ProductRepository;
using Ecommerce.Repository.Repositories.ProductVariationRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace Ecommerce.Repository.Repositories.ProductItemRepository
{
    public class ProductItemRepository : IProductItem
    {
        private readonly ApplicationDbContext _dbContext;
        public ProductItemRepository(ApplicationDbContext _dbContext
            )
        {
            this._dbContext = _dbContext;

        }
        public async Task<ProductItem> AddProductItemAsync(ProductItem productItem)
        {
            try
            {
                await _dbContext.ProductItem.AddAsync(productItem);
                await SaveChangesAsync();
                return productItem;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ProductItem> DeleteProductItemByIdAsync(Guid id)
        {
            try
            {
                ProductItem productItem = await GetProductItemByIdAsync(id);
                _dbContext.ProductItem.Attach(productItem);
                _dbContext.ProductItem.Remove(productItem);
                await SaveChangesAsync();
                return productItem;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ProductItem>> GetAllItemsAsync()
        {
            try
            {
                return await _dbContext.ProductItem.Include(e=>e.Product).Include(e=>e.ProductVariation2)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ProductItem>> GetAllProductItemsByProductIdAsync(Guid productId)
        {
            try
            {
                return
                    from p in await GetAllItemsAsync()
                    where p.ProductId == productId
                    select p;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ProductItem> GetProductItemByIdAsync(Guid id)
        {
            try
            {
                ProductItem? productItem = await _dbContext.ProductItem.Where(e => e.Id == id)
                    .FirstOrDefaultAsync();
                return productItem;
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

        public async Task<ProductItem> UpdateProductItemAsync(ProductItem productItem)
        {
            try
            {
                ProductItem productItem1 = await GetProductItemByIdAsync(productItem.Id);
                productItem1.Price = productItem.Price;
                productItem1.ProducItemImageUrl = productItem.ProducItemImageUrl;
                productItem1.QuantityInStock = productItem.QuantityInStock;
                productItem1.SKU = productItem.SKU;
                await SaveChangesAsync();
                return productItem1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ProductItem> UpsertAsync(ProductItem productItem)
        {
            try
            {
                ProductItem productItem1 = await GetProductItemByIdAsync(productItem.Id);
                if (productItem1 == null)
                {
                    return await AddProductItemAsync(productItem);
                }
                return await UpdateProductItemAsync(productItem);
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
