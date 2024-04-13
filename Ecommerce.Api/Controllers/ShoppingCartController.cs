using Ecommerce.Data.DTOs;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Service.Services.ShoppingCartService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;
        public ShoppingCartController(IShoppingCartService _shoppingCartService)
        {
            this._shoppingCartService = _shoppingCartService;
        }

        [HttpGet("getallshoppingcarts")]
        public async Task<IActionResult> GetAllShoppingCartAsync()
        {
            try
            {
                var response = await _shoppingCartService.GeAllShoppingCartsAsync();
                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<string>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message
                    });
            }
        }

        [HttpGet("getallshoppingcartsbyuserid/{userId}")]
        public async Task<IActionResult> GetAllShoppingCartAsync(string userId)
        {
            try
            {
                var response = await _shoppingCartService.GeAllShoppingCartsByUserIdAsync(userId);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<string>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message
                    });
            }
        }

        [HttpGet("getallshoppingcartsbyuserusernameoremail/{usernameOrEmail}")]
        public async Task<IActionResult> GetAllShoppingCartByUserUsernameOrEmailAsync(string usernameOrEmail)
        {
            try
            {
                var response = await _shoppingCartService.GeAllShoppingCartsByUsernameOrEmailAsync(usernameOrEmail);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<string>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message
                    });
            }
        }


        [HttpPost("addshoppingcart")]
        public async Task<IActionResult> AddShoppingCartAsync([FromBody] ShoppingCartDto shoppingCartDto)
        {
            try
            {
                var response = await _shoppingCartService.AddShoppingCartAsync(shoppingCartDto);
                

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<string>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message
                    });
            }
        }

        [HttpPut("updateshoppingcart")]
        public async Task<IActionResult> UpdateShoppingCartAsync([FromBody] ShoppingCartDto shoppingCartDto)
        {
            try
            {
                var response = await _shoppingCartService.UpdateShoppingCartAsync(shoppingCartDto);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<string>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message
                    });
            }
        }

        [HttpGet("getshoppingcartbyid/{shoppingCartId}")]
        public async Task<IActionResult> GetShoppingCartByIdAsync([FromRoute] Guid shoppingCartId)
        {
            try
            {
                var response = await _shoppingCartService.GetShoppingCartByIdAsync(shoppingCartId);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<string>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message
                    });
            }
        }

        [HttpDelete("deleteshoppingcartbyid/{shoppingCartId}")]
        public async Task<IActionResult> DeleteShoppingCartByIdAsync([FromRoute] Guid shoppingCartId)
        {
            try
            {
                var response = await _shoppingCartService.DeleteShoppingCartByIdAsync(shoppingCartId);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<string>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message
                    });
            }
        }



    }
}
