

using Ecommerce.Data;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Repository.Repositories.ProductItemRepository;
using Ecommerce.Repository.Repositories.VariationOptionsRepository;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository.Repositories.ProductVariationRepository
{
    public class ProductVariationRepository : IProductVariation
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductVariationRepository(ApplicationDbContext _dbContext
            )
        {
            this._dbContext = _dbContext;

        }

        public ProductVariation AddProductVariation(ProductVariation productVariation)
        {
            try
            {
                _dbContext.ProductVariation.Add(productVariation);
                SaveChanges();
                return productVariation;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ProductVariation DeleteProductVariationById(Guid id)
        {
            try
            {
                ProductVariation productVariation = GetProductVariationById(id);
                _dbContext.ProductVariation.Remove(productVariation);
                SaveChanges();
                return productVariation;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<ProductVariation> GetAllProductVariations()
        {
            try
            {
                return _dbContext.ProductVariation.Include(e=>e.ProductItem)
                    .Include(e=>e.VariationOption).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<ProductVariation> GetAllVariationsByProductItemId(Guid productItemId)
        {
            try
            {
                return
                    from v in GetAllProductVariations()
                    where v.ProductItemId == productItemId
                    select v;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<ProductVariation> GetAllVariationsByVariationOptionId(Guid variationOptionId)
        {
            try
            {
                return
                    from v in GetAllProductVariations()
                    where v.VariationOptionId == variationOptionId
                    select v;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ProductVariation GetProductVariationById(Guid id)
        {
            try
            {
                ProductVariation? productVariation = _dbContext.ProductVariation
                    .Where(e => e.Id == id).FirstOrDefault();
                if(productVariation == null)
                {
                    return null;
                }
                return productVariation;
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

        public ProductVariation UpdateProductVariation(ProductVariation productVariation)
        {
            try
            {
                ProductVariation productVariation1 = GetProductVariationById(productVariation.Id);
                productVariation1.ProductItemId = productVariation.ProductItemId;
                productVariation1.VariationOptionId = productVariation.VariationOptionId;
                SaveChanges();
                return productVariation1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ProductVariation Upsert(ProductVariation productVariation)
        {
            try
            {
                ProductVariation productVariation1 = GetProductVariationById(productVariation.Id);
                if(productVariation1 == null)
                {
                    return AddProductVariation(productVariation);
                }
                return UpdateProductVariation(productVariation);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
