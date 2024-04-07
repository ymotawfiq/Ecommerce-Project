using Ecommerce.Data;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Repository.Repositories.ProductCategoryRepository;
using Microsoft.EntityFrameworkCore;


namespace Ecommerce.Repository.Repositories.ProductRepository
{
    public class ProductRepository : IProduct
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IProductCategory _productCategoryRepository;
        public ProductRepository(ApplicationDbContext _dbContext, IProductCategory _productCategoryRepository)
        {
            this._dbContext = _dbContext;
            this._productCategoryRepository = _productCategoryRepository;
        }
        public Product AddProduct(Product product)
        {
            try
            {
                _dbContext.Product.Add(product);
                SaveChanges();
                return product;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Product DeleteProductById(Guid productId)
        {
            try
            {
                Product product = GetProductById(productId);
                _dbContext.Product.Remove(product);
                SaveChanges();
                return product;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                return _dbContext.Product.Include(e=>e.Category).Include(e=>e.ProductItems).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Product GetProductById(Guid productId)
        {
            try
            {
                Product? product = _dbContext.Product.Where(p => p.Id == productId).FirstOrDefault();
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

        public IEnumerable<Product> GetProductsByCategoryId(Guid CategoryId)
        {
            try
            {
                return (
                    from p in GetAllProducts()
                    where p.CategoryId == CategoryId
                    select p
                    );
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

        public Product UpdateProduct(Product product)
        {
            try
            {
                Product? product1 = GetProductById(product.Id);
                product1.Name = product.Name;
                product1.ProductImageUrl = product.ProductImageUrl;
                product1.CategoryId = product.CategoryId;
                product1.Description = product.Description;
                product1.Category = _productCategoryRepository.GetCategoryById(product.CategoryId);
                SaveChanges();
                return product1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Product Upsert(Product product)
        {
            try
            {
                Product? product1 = GetProductById(product.Id);
                if (product1 == null)
                {
                    return AddProduct(product);
                }
                return UpdateProduct(product);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
