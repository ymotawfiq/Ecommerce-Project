

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
        public ProductImages AddProductImages(ProductImages productImages)
        {
            try
            {
                _dbContext.ProductImages.Add(productImages);
                SaveChanges();
                return productImages;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ProductImages DeleteProductImages(Guid id)
        {
            try
            {
                ProductImages productImages = GetProductImagesById(id);
                _dbContext.ProductImages.Remove(productImages);
                SaveChanges();
                return productImages;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<ProductImages> GetAllProductsImages()
        {
            try
            {
                return _dbContext.ProductImages.Include(p=>p.Product).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ProductImages GetProductImagesById(Guid id)
        {
            try
            {
                ProductImages? productImages = _dbContext.ProductImages.Where(p => p.Id == id).FirstOrDefault();
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

        public IEnumerable<ProductImages> GetProductImagesByProductId(Guid productId)
        {
            try
            {
                return
                    from p in GetAllProductsImages()
                    where p.ProductId == productId
                    select p;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool RemoveImagesByProductId(Guid productId)
        {
            try
            {
                var images = _dbContext.ProductImages.Where(p => p.ProductId == productId);
                _dbContext.ProductImages.RemoveRange(images);
                SaveChanges();
                return true;
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

        public ProductImages UpdateProductImages(ProductImages productImages)
        {
            try
            {
                ProductImages productImages1 = GetProductImagesById(productImages.Id);
                productImages1.ProductImageUrl = productImages.ProductImageUrl;
                SaveChanges();
                return productImages1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ProductImages Upsert(ProductImages productImages)
        {
            try
            {
                ProductImages productImages1 = GetProductImagesById(productImages.Id);
                if(productImages1 == null)
                {
                    return AddProductImages(productImages);
                }
                return UpdateProductImages(productImages);
            }
            catch (Exception)
            {
                throw;
            }
        }

        
    }
}
