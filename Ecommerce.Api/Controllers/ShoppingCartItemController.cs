using Ecommerce.Data.DTOs;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Service.Services.ShoppingCartItemService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartItemController : ControllerBase
    {
        private readonly IShoppingCartItemService _shoppingCartItemService;
        public ShoppingCartItemController(IShoppingCartItemService _shoppingCartItemService)
        {
            this._shoppingCartItemService = _shoppingCartItemService;
        }

        [HttpGet("allshoppingcartitem")]
        public async Task<IActionResult> GetAllShoppingCartItemsAsync()
        {
            try
            {
                var response = await _shoppingCartItemService.GetAllShoppingCartItemsAsync();
                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = ex.Message
                });
            }
        }


        [HttpGet("allshoppingcartitembycartid/{cartId}")]
        public async Task<IActionResult> GetAllShoppingCartItemsAsync([FromRoute] Guid cartId)
        {
            try
            {
                var response = await _shoppingCartItemService.GetAllShoppingCartItemsByCartIdAsync(cartId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = ex.Message
                });
            }
        }


        [HttpPost("addshoppingcartitem")]
        public async Task<IActionResult> AddShoppingCartItemsAsync([FromBody] ShoppingCartItemDto shoppingCartItemDto)
        {
            try
            {
                var response = await _shoppingCartItemService.AddShoppingCartItemAsync(shoppingCartItemDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = ex.Message
                });
            }
        }


        [HttpPut("updateshoppingcartitem")]
        public async Task<IActionResult> UpdateShoppingCartItemsAsync([FromBody] ShoppingCartItemDto shoppingCartItemDto)
        {
            try
            {
                var response = await _shoppingCartItemService.UpdateShoppingCartItemAsync(shoppingCartItemDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = ex.Message
                });
            }
        }

        [HttpGet("getshoppingcartitembyid/{shoppingCartItemId}")]
        public async Task<IActionResult> UpdateShoppingCartItemsAsync([FromRoute] Guid shoppingCartItemId)
        {
            try
            {
                var response = await _shoppingCartItemService.GetShoppingCartItemAsync(shoppingCartItemId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = ex.Message
                });
            }
        }

        [HttpDelete("deleteshoppingcartitembyid/{shoppingCartItemId}")]
        public async Task<IActionResult> DeleteShoppingCartItemsAsync([FromRoute] Guid shoppingCartItemId)
        {
            try
            {
                var response = await _shoppingCartItemService.DeleteShoppingCartItemByIdAsync(shoppingCartItemId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = ex.Message
                });
            }
        }


    }
}
