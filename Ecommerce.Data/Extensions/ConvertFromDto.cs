

using Ecommerce.Data.DTOs;
using Ecommerce.Data.Models.Entities;

namespace Ecommerce.Data.Extensions
{
    public static class ConvertFromDto
    {
        public static ProductCategory ConvertFromProductCategoryDto_Add(ProductCategoryDto productCategoryDto)
        {
            return new ProductCategory
            {
                CategoryName = productCategoryDto.Name,
                ParentCategoryId = productCategoryDto.ParentCategoryId,
            };
        }

        public static ProductCategory ConvertFromProductCategoryDto_Update(ProductCategoryDto productCategoryDto)
        {
            if(productCategoryDto.Id == null)
            {
                throw new NullReferenceException("Category id must not be null");
            }
            return new ProductCategory
            {
                Id = new Guid(productCategoryDto.Id),
                CategoryName = productCategoryDto.Name,
                ParentCategoryId = productCategoryDto.ParentCategoryId,
            };
        }

        public static Product ConvertFromProductDto_Add(ProductDto productDto)
        {
            if (productDto.ProductImageUrl == null)
            {
                throw new NullReferenceException("Image url must not be null");
            }
            return new Product
            {
                CategoryId = productDto.CategoryId,
                Description = productDto.Description,
                Name = productDto.Name,
                ProductImageUrl = productDto.ProductImageUrl,
            };
        }

        public static Product ConvertFromProductDto_Update(ProductDto productDto)
        {
            if (productDto.Id == null)
            {
                throw new NullReferenceException("Category id must not be null");
            }
            if (productDto.ProductImageUrl == null)
            {
                throw new NullReferenceException("Image url must not be null");
            }
            return new Product
            {
                Id = new Guid(productDto.Id),
                CategoryId = productDto.CategoryId,
                Description = productDto.Description,
                Name = productDto.Name,
                ProductImageUrl = productDto.ProductImageUrl,
            };
        }


    }
}
