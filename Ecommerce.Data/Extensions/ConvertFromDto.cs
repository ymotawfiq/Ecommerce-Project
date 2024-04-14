

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

        public static Promotion ConvertFromPromotionDto_Add(PromotionDto promotionDto)
        {
            return new Promotion
            {
                Description = promotionDto.Description,
                DiscountRate = promotionDto.DiscountRate,
                EndDate = promotionDto.EndDate,
                Name = promotionDto.Name,
                StartDate = promotionDto.StartDate
            };
        }

        public static Promotion ConvertFromPromotionDto_Update(PromotionDto promotionDto)
        {
            if (promotionDto.Id == null)
            {
                throw new NullReferenceException("Promotion id must not be null");
            }
            return new Promotion
            {
                Id = new Guid(promotionDto.Id),
                Description = promotionDto.Description,
                DiscountRate = promotionDto.DiscountRate,
                EndDate = promotionDto.EndDate,
                Name = promotionDto.Name,
                StartDate = promotionDto.StartDate
            };
        }


        public static PromotionCategory ConvertFromPromotionCategoryDto_Add
            (PromotionCategoryDto promotionCategoryDto)
        {
            return new PromotionCategory
            {
                CategoryId = promotionCategoryDto.CategoryId,
                PromotionId = promotionCategoryDto.PromotionId
            };
        }

        public static PromotionCategory ConvertFromPromotionCategoryDto_Update
            (PromotionCategoryDto promotionCategoryDto)
        {
            if (promotionCategoryDto.Id == null)
            {
                throw new NullReferenceException("Promotion category id must not be null");
            }
            return new PromotionCategory
            {
                Id = new Guid(promotionCategoryDto.Id),
                CategoryId = promotionCategoryDto.CategoryId,
                PromotionId = promotionCategoryDto.PromotionId
            };
        }


        public static Countary ConvertFromCountaryDto_Add(CountaryDto countaryDto)
        {
            return new Countary
            {
                Name = countaryDto.Name
            };
        }

        public static Countary ConvertFromCountaryDto_Update(CountaryDto countaryDto)
        {
            if (countaryDto.Id == null)
            {
                throw new NullReferenceException("Country id must not be null");
            }
            return new Countary
            {
                Id = new Guid(countaryDto.Id),
                Name = countaryDto.Name
            };
        }


        public static Address ConvertFromAddressDto_Add(AddressDto addressDto)
        {
            return new Address
            {
                AddressLine1 = addressDto.AddressLine1,
                AddressLine2 = addressDto.AddressLine2,
                City = addressDto.City,
                CountaryId = addressDto.CountaryId,
                PostalCode = addressDto.PostalCode,
                Region = addressDto.Region,
                StreetNumber = addressDto.StreetNumber,
                UnitNumber = addressDto.UnitNumber
            };
        }

        public static Address ConvertFromAddressDto_Update(AddressDto addressDto)
        {
            if (addressDto.Id == null)
            {
                throw new NullReferenceException("Address id must not be null");
            }
            return new Address
            {
                Id = new Guid(addressDto.Id),
                AddressLine1 = addressDto.AddressLine1,
                AddressLine2 = addressDto.AddressLine2,
                City = addressDto.City,
                CountaryId = addressDto.CountaryId,
                PostalCode = addressDto.PostalCode,
                Region = addressDto.Region,
                StreetNumber = addressDto.StreetNumber,
                UnitNumber = addressDto.UnitNumber
            };
        }

        public static UserAddress ConvertFromUserAddressDto_Add(UserAddressDto userAddressDto)
        {
            return new UserAddress
            {
                AddressId = new Guid(userAddressDto.AddressId),
                UserId = userAddressDto.UserIdOrEmail,
                IsDefault = userAddressDto.IsDefault
            };
        }

        public static UserAddress ConvertFromUserAddressDto_Update(UserAddressDto userAddressDto)
        {
            if(userAddressDto.Id == null)
            {
                throw new NullReferenceException("User address id must not be null");
            }
            return new UserAddress
            {
                Id = new Guid(userAddressDto.Id),
                AddressId = new Guid(userAddressDto.AddressId),
                UserId = userAddressDto.UserIdOrEmail,
                IsDefault = userAddressDto.IsDefault
            };
        }


        public static PaymentType ConvertFromPaymentTypeDto_Add(PaymentTypeDto paymentTypeDto)
        {
            return new PaymentType
            {
                Value = paymentTypeDto.Value
            };
        }

        public static PaymentType ConvertFromPaymentTypeDto_Update(PaymentTypeDto paymentTypeDto)
        {
            if (paymentTypeDto.Id == null)
            {
                throw new NullReferenceException("Payment type id must not be null");
            }
            return new PaymentType
            {
                Id = new Guid(paymentTypeDto.Id),
                Value = paymentTypeDto.Value
            };
        }


        public static ShoppingCartItem ConvertFromShoppingCartItemDto_Add(ShoppingCartItemDto shoppingCartItemDto)
        {
            return new ShoppingCartItem
            {
                CartId = shoppingCartItemDto.CartId,
                ProductItemId = shoppingCartItemDto.ProductItemId,
                Qty = shoppingCartItemDto.Qty
            };
        }

        public static ShoppingCartItem ConvertFromShoppingCartItemDto_Update(ShoppingCartItemDto shoppingCartItemDto)
        {
            if (shoppingCartItemDto.Id == null)
            {
                throw new NullReferenceException("Shopping cart id must not be null");
            }
            return new ShoppingCartItem
            {
                Id = new Guid(shoppingCartItemDto.Id),
                CartId = shoppingCartItemDto.CartId,
                ProductItemId = shoppingCartItemDto.ProductItemId,
                Qty = shoppingCartItemDto.Qty
            };
        }

        public static ShippingMethod ConvertFromShippingMethodDto_Add(ShippingMethodDto shippingMethodDto)
        {
            return new ShippingMethod
            {
                Name = shippingMethodDto.Name,
                Price = shippingMethodDto.Price
            };
        }

        public static ShippingMethod ConvertFromShippingMethodDto_Update(ShippingMethodDto shippingMethodDto)
        {
            if (shippingMethodDto.Id == null)
            {
                throw new NullReferenceException("Shipping method id must not be null");
            }
            return new ShippingMethod
            {
                Id = new Guid(shippingMethodDto.Id),
                Name = shippingMethodDto.Name,
                Price = shippingMethodDto.Price
            };
        }


        public static OrderStatus ConvertFromOrderStatusDto_Add(OrderStatusDto orderStatusDto)
        {
            return new OrderStatus
            {
                Status = orderStatusDto.Status
            };
        }

        public static OrderStatus ConvertFromOrderStatusDto_Update(OrderStatusDto orderStatusDto)
        {
            if(orderStatusDto.Id == null)
            {
                throw new NullReferenceException("Order status id must not be null");
            }
            return new OrderStatus
            {
                Id = new Guid(orderStatusDto.Id),
                Status = orderStatusDto.Status
            };
        }


        public static OrderLine ConvertFromOrderLineDto_Add(OrderLineDto orderLineDto)
        {
            return new OrderLine
            {
                ProductItemId = orderLineDto.ProductItemId,
                Qty = orderLineDto.Qty,
                ShopOrderId = orderLineDto.ShopOrderId,
                Price = orderLineDto.Price
            };
        }

        public static OrderLine ConvertFromOrderLineDto_Update(OrderLineDto orderLineDto)
        {
            if (orderLineDto.Id == null)
            {
                throw new NullReferenceException("Order Line id must not be null");
            }
            return new OrderLine
            {
                Id = new Guid(orderLineDto.Id),
                ProductItemId = orderLineDto.ProductItemId,
                Qty = orderLineDto.Qty,
                ShopOrderId = orderLineDto.ShopOrderId,
                Price = orderLineDto.Price
            };
        }



    }
}
