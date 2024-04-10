

using Ecommerce.Data.DTOs;
using Ecommerce.Data.Extensions;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Repository.Repositories.ProductItemRepository;
using Ecommerce.Repository.Repositories.ProductRepository;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Service.Services.ProductItemService
{
    public class ProductItemService : IProductItemService
    {
        private readonly IProductItem _productItemRepository;
        private readonly IProduct _productRepository;
        public ProductItemService(IProductItem _productItemRepository, IProduct _productRepository)
        {
            this._productItemRepository = _productItemRepository;
            this._productRepository = _productRepository;
        }

        public async Task<ApiResponse<IEnumerable<ProductItem>>> GetAllItemsAsync()
        {
            var items = await _productItemRepository.GetAllItemsAsync();
            if (items.ToList().Count == 0)
            {
                return new ApiResponse<IEnumerable<ProductItem>>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "No items found",
                    ResponseObject = items
                };
            }
            return new ApiResponse<IEnumerable<ProductItem>>
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Items founded successfully",
                ResponseObject = items
            };
        }

        public async Task<ApiResponse<IEnumerable<ProductItem>>> GetAllItemsByProductIdAsync(Guid productId)
        {
            var items = await _productItemRepository.GetAllProductItemsByProductIdAsync(productId);
            if (items.ToList().Count == 0)
            {
                return new ApiResponse<IEnumerable<ProductItem>>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "This product does not contain any items",
                    ResponseObject = items
                };
            }
            return new ApiResponse<IEnumerable<ProductItem>>
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Items founded successfully",
                ResponseObject = items
            };
        }

        public async Task<ApiResponse<ProductItem>> AddItemAsync(ProductItemDto productItemDto)
        {
            if (productItemDto == null)
            {
                return new ApiResponse<ProductItem>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Input must not be null",
                    ResponseObject = new ProductItem()
                };
            }
            var product = await _productRepository.GetProductByIdAsync(new Guid(productItemDto.ProductId));
            if (product == null)
            {
                return new ApiResponse<ProductItem>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = $"Product with id ({productItemDto.ProductId}) not exists",
                    ResponseObject = new ProductItem()
                };
            }
            string imageUrl = SaveProductImage(productItemDto);
            productItemDto.ImageUrl = imageUrl;
            if (imageUrl == null)
            {
                return new ApiResponse<ProductItem>
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = $"Can't save item",
                    ResponseObject = new ProductItem()
                };
            }
            var item = await _productItemRepository.AddProductItemAsync(ConvertFromDto
                .ConvertFromProductItemDto_Add(productItemDto));
            return new ApiResponse<ProductItem>
            {
                StatusCode = 201,
                IsSuccess = true,
                Message = "Item created successfully",
                ResponseObject = item
            };
        }

        public async Task<ApiResponse<ProductItem>> UpdateItemAsync(ProductItemDto productItemDto)
        {
            if (productItemDto == null)
            {
                return new ApiResponse<ProductItem>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Input must not be null",
                    ResponseObject = new ProductItem()
                };
            }
            if (productItemDto.Id == null)
            {
                return new ApiResponse<ProductItem>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "You must enter item id",
                    ResponseObject = new ProductItem()
                };
            }
            var oldItem = await _productItemRepository.GetProductItemByIdAsync(new Guid(productItemDto.Id));
            if (oldItem == null)
            {
                return new ApiResponse<ProductItem>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = $"Item with id ({productItemDto.Id}) not exists",
                    ResponseObject = new ProductItem()
                };
            }
            bool isImageDeleted = DeleteExistingItemImage(oldItem.ProducItemImageUrl);

            if (!isImageDeleted)
            {
                return new ApiResponse<ProductItem>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = $"Can't update item",
                    ResponseObject = new ProductItem()
                };
            }
            string imageUrl = SaveProductImage(productItemDto);
            productItemDto.ImageUrl = imageUrl;
            if (imageUrl == null)
            {
                return new ApiResponse<ProductItem>
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = $"Can't save item",
                    ResponseObject = new ProductItem()
                };
            }
            var newItem = await _productItemRepository.UpdateProductItemAsync(ConvertFromDto
                .ConvertFromProductItemDto_Update(productItemDto));
            return new ApiResponse<ProductItem>
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Item updated successfully",
                ResponseObject = newItem
            };
        }

        public async Task<ApiResponse<ProductItem>> DeleteItemAsync(Guid itemId)
        {
            var oldItem = await _productItemRepository.GetProductItemByIdAsync(itemId);
            if (oldItem == null)
            {
                return new ApiResponse<ProductItem>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = $"Item with id ({itemId}) not exists",
                    ResponseObject = new ProductItem()
                };
            }
            bool isImageDeleted = DeleteExistingItemImage(oldItem.ProducItemImageUrl);

            if (!isImageDeleted)
            {
                return new ApiResponse<ProductItem>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = $"Can't delete item",
                    ResponseObject = new ProductItem()
                };
            }

            var deletedItem = await _productItemRepository.DeleteProductItemByIdAsync(itemId);
            return new ApiResponse<ProductItem>
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Item deleted successfully",
                ResponseObject = deletedItem
            };
        }

        public async Task<ApiResponse<ProductItem>> GetItemByIdAsync(Guid itemId)
        {
            var oldItem = await _productItemRepository.GetProductItemByIdAsync(itemId);
            if (oldItem == null)
            {
                return new ApiResponse<ProductItem>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = $"Item with id ({itemId}) not exists",
                    ResponseObject = new ProductItem()
                };
            }
            return new ApiResponse<ProductItem>
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Item founded successfully",
                ResponseObject = oldItem
            };
        }


        // Save Image Url
        private string SaveProductImage(ProductItemDto productItemDto)
        {
            string uniqueFileName = null;
            if (productItemDto.Image != null)
            {
                string path = @"D:\my_source_code\C sharp\EcommerceProjectSolution\Ecommerce.Client\wwwroot";

                string uploadsFolder = Path.Combine(path, @"Images\ProductItems");

                uniqueFileName = Guid.NewGuid().ToString().Substring(0, 8)
                    + "_" + productItemDto.Image.FileName;

                if (!System.IO.Directory.Exists(uploadsFolder))
                {
                    System.IO.Directory.CreateDirectory(uploadsFolder);
                }

                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    productItemDto.Image.CopyTo(fileStream);
                    fileStream.Flush();
                }
            }
            return uniqueFileName;
        }

        private bool DeleteExistingItemImage(string imageUrl)
        {
            string path = @"D:/my_source_code/C sharp/EcommerceProjectSolution/Ecommerce.Client/wwwroot/Images/ProductItems/";
            string existingImage = Path.Combine(path, $"{imageUrl}");
            if (System.IO.File.Exists(existingImage))
            {
                System.IO.File.Delete(existingImage);
                return true;
            }
            return false;
        }
    }
}
