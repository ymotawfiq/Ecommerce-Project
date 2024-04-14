using Ecommerce.Data.DTOs;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities.Authentication;
using Ecommerce.Repository.Repositories.ShoppingCartRepository;
using Ecommerce.Service.Services.ShoppingCartItemService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartItemController : ControllerBase
    {
        private readonly IShoppingCartItemService _shoppingCartItemService;
        private readonly UserManager<SiteUser> _userManager;
        private readonly IShoppingCart _shoppingCartRepository;
        public ShoppingCartItemController(IShoppingCartItemService _shoppingCartItemService,
            UserManager<SiteUser> _userManager, IShoppingCart _shoppingCartRepository)
        {
            this._shoppingCartItemService = _shoppingCartItemService;
            this._userManager = _userManager;
            this._shoppingCartRepository = _shoppingCartRepository;
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin,User")]
        [HttpGet("allshoppingcartitembycartid/{cartId}")]
        public async Task<IActionResult> GetAllShoppingCartItemsByCartIdAsync([FromRoute] Guid cartId)
        {
            try
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                if (user != null)
                {
                    var shoppingCart = await _shoppingCartRepository.GetShoppingCartByIdAsync(cartId);
                    var admins = await _userManager.GetUsersInRoleAsync("Admin");
                    if(shoppingCart.UserId == user.Id || admins.Contains(user))
                    {
                        var response = await _shoppingCartItemService
                            .GetAllShoppingCartItemsByCartIdAsync(cartId);
                        return Ok(response);
                    }
                }
                return Unauthorized();
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

        [Authorize(Roles = "Admin,User")]
        [HttpPost("addshoppingcartitem")]
        public async Task<IActionResult> AddShoppingCartItemsAsync([FromBody] ShoppingCartItemDto shoppingCartItemDto)
        {
            try
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                if (user != null)
                {
                    if (shoppingCartItemDto.Id == null)
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<string>
                        {
                            StatusCode = 400,
                            IsSuccess = false,
                            Message = "Shopping cart item id must not be null"
                        });
                    }
                    var shoppingCart = await _shoppingCartRepository
                        .GetShoppingCartByIdAsync(new Guid(shoppingCartItemDto.Id));
                    var admins = await _userManager.GetUsersInRoleAsync("Admin");
                    if (shoppingCart.UserId == user.Id || admins.Contains(user))
                    {
                        var response = await _shoppingCartItemService
                            .AddShoppingCartItemAsync(shoppingCartItemDto);
                        return Ok(response);
                    }
                }
                return Unauthorized();

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

        [Authorize(Roles = "Admin,User")]
        [HttpPut("updateshoppingcartitem")]
        public async Task<IActionResult> UpdateShoppingCartItemsAsync([FromBody] ShoppingCartItemDto shoppingCartItemDto)
        {
            try
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                if (user != null)
                {
                    if (shoppingCartItemDto.Id == null)
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<string>
                        {
                            StatusCode = 400,
                            IsSuccess = false,
                            Message = "Shopping cart item id must not be null"
                        });
                    }
                    var shoppingCart = await _shoppingCartRepository
                        .GetShoppingCartByIdAsync(new Guid(shoppingCartItemDto.Id));
                    var admins = await _userManager.GetUsersInRoleAsync("Admin");
                    if (shoppingCart.UserId == user.Id || admins.Contains(user))
                    {
                        var response = await _shoppingCartItemService
                            .UpdateShoppingCartItemAsync(shoppingCartItemDto);
                        return Ok(response);
                    }
                }
                return Unauthorized();

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

        [Authorize(Roles = "Admin,User")]
        [HttpGet("getshoppingcartitembyid/{shoppingCartItemId}")]
        public async Task<IActionResult> UpdateShoppingCartItemsAsync([FromRoute] Guid shoppingCartItemId)
        {
            try
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                if (user != null)
                {
                    var shoppingCart = await _shoppingCartRepository
                        .GetShoppingCartByIdAsync(shoppingCartItemId);
                    var admins = await _userManager.GetUsersInRoleAsync("Admin");
                    if (shoppingCart.UserId == user.Id || admins.Contains(user))
                    {
                        var response = await _shoppingCartItemService
                            .GetShoppingCartItemAsync(shoppingCartItemId);
                        return Ok(response);
                    }
                }
                return Unauthorized();
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

        [Authorize(Roles = "Admin")]
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
