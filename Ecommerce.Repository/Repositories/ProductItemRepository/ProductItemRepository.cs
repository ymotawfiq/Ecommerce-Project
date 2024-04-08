

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
        public ProductItem AddProductItem(ProductItem productItem)
        {
            try
            {
                _dbContext.ProductItem.Add(productItem);
                SaveChanges();
                return productItem;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ProductItem DeleteProductItemById(Guid id)
        {
            try
            {
                ProductItem productItem = GetProductItemById(id);
                _dbContext.ProductItem.Remove(productItem);
                SaveChanges();
                return productItem;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<ProductItem> GetAllItems()
        {
            try
            {
                return _dbContext.ProductItem.Include(e=>e.Product).Include(e=>e.ProductVariation2)
                    .ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<ProductItem> GetAllProductItemsByProductId(Guid productId)
        {
            try
            {
                return
                    from p in GetAllItems()
                    where p.ProductId == productId
                    select p;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ProductItem GetProductItemById(Guid id)
        {
            try
            {
                ProductItem? productItem = _dbContext.ProductItem.Where(e => e.Id == id).FirstOrDefault();
                if (productItem == null)
                {
                    return null;
                }
                return productItem;
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

        public ProductItem UpdateProductItem(ProductItem productItem)
        {
            try
            {
                ProductItem productItem1 = GetProductItemById(productItem.Id);
                productItem1.Price = productItem.Price;
                productItem1.ProducItemImageUrl = productItem.ProducItemImageUrl;
                productItem1.QuantityInStock = productItem.QuantityInStock;
                productItem1.SKU = productItem.SKU;
                SaveChanges();
                return productItem1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ProductItem Upsert(ProductItem productItem)
        {
            try
            {
                ProductItem productItem1 = GetProductItemById(productItem.Id);
                if (productItem1 == null)
                {
                    return AddProductItem(productItem);
                }
                return UpdateProductItem(productItem);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
