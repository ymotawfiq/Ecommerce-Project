using Ecommerce.Data.DTOs;
using Ecommerce.Data.Extensions;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Repository.Repositories.ProductItemRepository;
using Ecommerce.Repository.Repositories.ProductRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductItemController : ControllerBase
    {
        private readonly IProductItem _productItemRepository;
        private readonly IProduct _productRepository;
        public ProductItemController(IProductItem _productItemRepository, IProduct _productRepository)
        {
            this._productItemRepository = _productItemRepository;
            this._productRepository = _productRepository;
        }

        [HttpGet("allitems")]
        public async Task<IActionResult> GetAllItemsAsync()
        {
            try
            {
                var items = await _productItemRepository.GetAllItemsAsync();
                if (items.ToList().Count == 0)
                {
                    return StatusCode(StatusCodes.Status200OK, new ApiResponse<IEnumerable<ProductItem>>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = "No items found",
                        ResponseObject = items
                    });
                }
                return StatusCode(StatusCodes.Status200OK, new ApiResponse<IEnumerable<ProductItem>>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Items founded successfully",
                    ResponseObject = items
                });
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<IEnumerable<ProductItem>>
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = ex.Message,
                    ResponseObject = new List<ProductItem>()
                });
            }
        }

        [HttpGet("allitemsbyproductid/{productId}")]
        public async Task<IActionResult> GetAllItemsByProductIdAsync([FromRoute] Guid productId)
        {
            try
            {
                var items = await _productItemRepository.GetAllProductItemsByProductIdAsync(productId);
                if (items.ToList().Count == 0)
                {
                    return StatusCode(StatusCodes.Status200OK, new ApiResponse<IEnumerable<ProductItem>>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = "This product does not contain any items",
                        ResponseObject = items
                    });
                }
                return StatusCode(StatusCodes.Status200OK, new ApiResponse<IEnumerable<ProductItem>>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Items founded successfully",
                    ResponseObject = items
                });
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<IEnumerable<ProductItem>>
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = ex.Message,
                    ResponseObject = new List<ProductItem>()
                });
            }
        }

        [HttpPost("additem")]
        public async Task<IActionResult> AddItemAsync([FromForm]ProductItemDto productItemDto)
        {
            try
            {
                if (productItemDto == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<ProductItem>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Input must not be null",
                        ResponseObject = new ProductItem()
                    });
                }
                var product = await _productRepository.GetProductByIdAsync(new Guid(productItemDto.ProductId));
                if(product == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<ProductItem>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = $"Product with id ({productItemDto.ProductId}) not exists",
                        ResponseObject = new ProductItem()
                    });
                }
                string imageUrl = SaveProductImage(productItemDto);
                productItemDto.ImageUrl = imageUrl;
                if (imageUrl == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<ProductItem>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = $"Can't save item",
                        ResponseObject = new ProductItem()
                    });
                }
                var item = await _productItemRepository.AddProductItemAsync(ConvertFromDto
                    .ConvertFromProductItemDto_Add(productItemDto));
                return StatusCode(StatusCodes.Status201Created, new ApiResponse<ProductItem>
                {
                    StatusCode = 201,
                    IsSuccess = true,
                    Message = "Item created successfully",
                    ResponseObject = item
                });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<ProductItem>
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = ex.Message,
                    ResponseObject = new ProductItem()
                });
            }
        }

        [HttpPut("updateitem")]
        public async Task<IActionResult> UpdateItemAsync([FromForm] ProductItemDto productItemDto)
        {
            try
            {
                if (productItemDto == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<ProductItem>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Input must not be null",
                        ResponseObject = new ProductItem()
                    });
                }
                if (productItemDto.Id == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<ProductItem>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "You must enter item id",
                        ResponseObject = new ProductItem()
                    });
                }
                var oldItem = await _productItemRepository.GetProductItemByIdAsync(new Guid(productItemDto.Id));
                if (oldItem == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<ProductItem>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = $"Item with id ({productItemDto.Id}) not exists",
                        ResponseObject = new ProductItem()
                    });
                }
                bool isImageDeleted = DeleteExistingItemImage(oldItem.ProducItemImageUrl);

                if (!isImageDeleted)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<ProductItem>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = $"Can't update item",
                        ResponseObject = new ProductItem()
                    });
                }
                string imageUrl = SaveProductImage(productItemDto);
                productItemDto.ImageUrl = imageUrl;
                if (imageUrl == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<ProductItem>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = $"Can't save item",
                        ResponseObject = new ProductItem()
                    });
                }
                var newItem = await _productItemRepository.UpdateProductItemAsync(ConvertFromDto
                    .ConvertFromProductItemDto_Update(productItemDto));
                return StatusCode(StatusCodes.Status200OK, new ApiResponse<ProductItem>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Item updated successfully",
                    ResponseObject = newItem
                });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<ProductItem>
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = ex.Message,
                    ResponseObject = new ProductItem()
                });
            }
        }

        [HttpDelete("deleteitem/{itemId}")]
        public async Task<IActionResult> DeleteItemAsync([FromRoute] Guid itemId)
        {
            try
            {
                var oldItem = await _productItemRepository.GetProductItemByIdAsync(itemId);
                if (oldItem == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<ProductItem>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = $"Item with id ({itemId}) not exists",
                        ResponseObject = new ProductItem()
                    });
                }
                bool isImageDeleted =DeleteExistingItemImage(oldItem.ProducItemImageUrl);

                if (!isImageDeleted)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<ProductItem>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = $"Can't delete item",
                        ResponseObject = new ProductItem()
                    });
                }

                var deletedItem = await _productItemRepository.DeleteProductItemByIdAsync(itemId);
                return StatusCode(StatusCodes.Status200OK, new ApiResponse<ProductItem>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Item deleted successfully",
                    ResponseObject = deletedItem
                });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<ProductItem>
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = ex.Message,
                    ResponseObject = new ProductItem()
                });
            }
        }

        [HttpGet("getitem/{itemId}")]
        public async Task<IActionResult> GetItemByIdAsync([FromRoute] Guid itemId)
        {
            try
            {
                var oldItem = await _productItemRepository.GetProductItemByIdAsync(itemId);
                if (oldItem == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<ProductItem>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = $"Item with id ({itemId}) not exists",
                        ResponseObject = new ProductItem()
                    });
                }
                return StatusCode(StatusCodes.Status200OK, new ApiResponse<ProductItem>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Item founded successfully",
                    ResponseObject = oldItem
                });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<ProductItem>
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = ex.Message,
                    ResponseObject = new ProductItem()
                });
            }
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
