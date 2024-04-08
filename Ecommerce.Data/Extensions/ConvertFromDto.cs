

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
            return new Product
            {
                CategoryId = productDto.CategoryId,
                Description = productDto.Description,
                Name = productDto.Name,
            };
        }

        public static Product ConvertFromProductDto_Update(ProductDto productDto)
        {
            if (productDto.Id == null)
            {
                throw new NullReferenceException("Category id must not be null");
            }
            return new Product
            {
                Id = new Guid(productDto.Id),
                CategoryId = productDto.CategoryId,
                Description = productDto.Description,
                Name = productDto.Name,
            };
        }


        public static ProductItem ConvertFromProductItemDto_Add(ProductItemDto productItemDto)
        {
            if (productItemDto.ImageUrl == null)
            {
                throw new NullReferenceException("Item image must not be null");
            }
            return new ProductItem
            {
                Price = productItemDto.Price,
                ProducItemImageUrl = productItemDto.ImageUrl,
                ProductId = new Guid(productItemDto.ProductId),
                QuantityInStock = productItemDto.QuantityInStock,
                SKU = productItemDto.SKU
            };
        }

        public static ProductItem ConvertFromProductItemDto_Update(ProductItemDto productItemDto)
        {
            if (productItemDto.Id == null || productItemDto.ImageUrl==null)
            {
                throw new NullReferenceException("Item image must not be null");
            }
            return new ProductItem
            {
                Id = new Guid(productItemDto.Id),
                Price = productItemDto.Price,
                ProducItemImageUrl = productItemDto.ImageUrl,
                ProductId = new Guid(productItemDto.ProductId),
                QuantityInStock = productItemDto.QuantityInStock,
                SKU = productItemDto.SKU
            };
        }


        public static Variation ConvertFromVariationDto_Add(VariationDto variationDto)
        {
            return new Variation
            {
                CategoryId = new Guid(variationDto.CatrgoryId),
                Name = variationDto.Name
            };
        }

        public static Variation ConvertFromVariationDto_Update(VariationDto variationDto)
        {
            if (variationDto.Id == null)
            {
                throw new NullReferenceException("Variation id must not be null");
            }
            return new Variation
            {
                Id = new Guid(variationDto.Id),
                CategoryId = new Guid(variationDto.CatrgoryId),
                Name = variationDto.Name
            };
        }

        public static VariationOptions ConvertFromVariationOptions_Add(VariationOptionsDto variationOptions)
        {
            return new VariationOptions
            {
                VariationId = new Guid(variationOptions.VariationId),
                Value = variationOptions.Value
            };
        }

        public static VariationOptions ConvertFromVariationOptions_Update(VariationOptionsDto variationOptions)
        {
            if (variationOptions.Id == null)
            {
                throw new NullReferenceException("Variation option id must not be null");
            }
            return new VariationOptions
            {
                Id = new Guid(variationOptions.Id),
                VariationId = new Guid(variationOptions.VariationId),
                Value = variationOptions.Value
            };
        }

        public static ProductVariation ConvertFromProductVariationsDto_Add(ProductVariationDto productVariationDto)
        {
            return new ProductVariation
            {
                ProductItemId = productVariationDto.ProductItemId,
                VariationOptionId = productVariationDto.VariationOptionId
            };
        }

        public static ProductVariation ConvertFromProductVariationsDto_Update(ProductVariationDto productVariationDto)
        {
            if (productVariationDto.Id == null)
            {
                throw new NullReferenceException("Product variation id must not be null");
            }
            return new ProductVariation
            {
                Id = new Guid(productVariationDto.Id),
                ProductItemId = productVariationDto.ProductItemId,
                VariationOptionId = productVariationDto.VariationOptionId
            };
        }

    }
}
