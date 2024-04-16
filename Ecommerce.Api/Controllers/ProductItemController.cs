using Ecommerce.Data.DTOs;
using Ecommerce.Data.Extensions;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Repository.Repositories.ProductItemRepository;
using Ecommerce.Repository.Repositories.ProductRepository;
using Ecommerce.Service.Services.ProductItemService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [ApiController]
    public class ProductItemController : ControllerBase
    {
        private readonly IProductItemService _productItemService;
        public ProductItemController(IProductItemService _productItemService)
        {
            this._productItemService = _productItemService;
        }

        [AllowAnonymous]
        [HttpGet("all-items")]
        public async Task<IActionResult> GetAllItemsAsync()
        {
            try
            {
                var response = await _productItemService.GetAllItemsAsync();
                return Ok(response);
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

        [AllowAnonymous]
        [HttpGet("allItemsByProduct/{productId}")]
        public async Task<IActionResult> GetAllItemsByProductIdAsync([FromRoute] Guid productId)
        {
            try
            {
                var response = await _productItemService.GetAllItemsByProductIdAsync(productId);
                return Ok(response);
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

        [Authorize(Roles = "Admin")]
        [HttpPost("add-item")]
        public async Task<IActionResult> AddItemAsync([FromForm]ProductItemDto productItemDto)
        {
            try
            {
                var response = await _productItemService.AddItemAsync(productItemDto);
                return Ok(response);

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

        [Authorize(Roles = "Admin")]
        [HttpPut("update-item")]
        public async Task<IActionResult> UpdateItemAsync([FromForm] ProductItemDto productItemDto)
        {
            try
            {

                var response = await _productItemService.UpdateItemAsync(productItemDto);
                return Ok(response);

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

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-item/{itemId}")]
        public async Task<IActionResult> DeleteItemAsync([FromRoute] Guid itemId)
        {
            try
            {
                var response = await _productItemService.DeleteItemAsync(itemId);
                return Ok(response);

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

        [AllowAnonymous]
        [HttpGet("item/{itemId}")]
        public async Task<IActionResult> GetItemByIdAsync([FromRoute] Guid itemId)
        {
            try
            {
                var response = await _productItemService.GetItemByIdAsync(itemId);
                return Ok(response);

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
