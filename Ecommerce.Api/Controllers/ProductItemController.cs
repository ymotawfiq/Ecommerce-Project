using Ecommerce.Data.DTOs;
using Ecommerce.Data.Extensions;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Repository.Repositories.ProductItemRepository;
using Ecommerce.Repository.Repositories.ProductRepository;
using Ecommerce.Service.Services.ProductItemService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductItemController : ControllerBase
    {
        private readonly IProductItemService _productItemService;
        public ProductItemController(IProductItemService _productItemService)
        {
            this._productItemService = _productItemService;
        }

        [HttpGet("allitems")]
        public async Task<IActionResult> GetAllItemsAsync()
        {
            try
            {
                var response = await _productItemService.GetAllItemsAsync();
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<IEnumerable<Address>>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Unknown error",
                        ResponseObject = new List<Address>()
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
                var response = await _productItemService.GetAllItemsByProductIdAsync(productId);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<IEnumerable<Address>>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Unknown error",
                        ResponseObject = new List<Address>()
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
                var response = await _productItemService.AddItemAsync(productItemDto);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<Address>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Unknown error",
                        ResponseObject = new Address()
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

                var response = await _productItemService.UpdateItemAsync(productItemDto);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<Address>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Unknown error",
                        ResponseObject = new Address()
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
                var response = await _productItemService.DeleteItemAsync(itemId);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<Address>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Unknown error",
                        ResponseObject = new Address()
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
                var response = await _productItemService.GetItemByIdAsync(itemId);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<Address>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Unknown error",
                        ResponseObject = new Address()
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



        
    }
}
