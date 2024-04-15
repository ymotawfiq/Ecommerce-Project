using Ecommerce.Data.DTOs;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities.Authentication;
using Ecommerce.Repository.Repositories.ShoppingCartRepository;
using Ecommerce.Service.Services.ShoppingCartService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly UserManager<SiteUser> _userManager;
        private readonly IShoppingCart _shoppingCartRepository;
        public ShoppingCartController(IShoppingCartService _shoppingCartService
            , UserManager<SiteUser> _userManager, IShoppingCart _shoppingCartRepository)
        {
            this._shoppingCartService = _shoppingCartService;
            this._userManager = _userManager;
            this._shoppingCartRepository = _shoppingCartRepository;
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin,User")]
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

        [Authorize(Roles = "Admin,User")]
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

        [Authorize(Roles = "Admin,User")]
        [HttpPost("addshoppingcart")]
        public async Task<IActionResult> AddShoppingCartAsync([FromBody] ShoppingCartDto shoppingCartDto)
        {
            try
            {
                if (HttpContext.User.Identity != null && HttpContext.User.Identity.Name != null)
                {
                    var user = await _userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
                    if (user != null)
                    {
                        var admins = await _userManager.GetUsersInRoleAsync("Admin");
                        if (user.Email == shoppingCartDto.UserIdOrEmail || user.Id == shoppingCartDto.UserIdOrEmail
                            || admins.Contains(user))
                        {
                            var response = await _shoppingCartService.AddShoppingCartAsync(shoppingCartDto);


                            return Ok(response);
                        }
                    }
                }
                
                return Unauthorized();
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

        [Authorize(Roles = "Admin,User")]
        [HttpPut("updateshoppingcart")]
        public async Task<IActionResult> UpdateShoppingCartAsync([FromBody] ShoppingCartDto shoppingCartDto)
        {
            try
            {

                if (HttpContext.User.Identity != null && HttpContext.User.Identity.Name != null)
                {
                    var user = await _userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
                    if (user != null)
                    {
                        var admins = await _userManager.GetUsersInRoleAsync("Admin");
                        if (user.Email == shoppingCartDto.UserIdOrEmail || user.Id == shoppingCartDto.UserIdOrEmail
                            || admins.Contains(user))
                        {
                            var response = await _shoppingCartService.UpdateShoppingCartAsync(shoppingCartDto);

                            return Ok(response);
                        }
                    }
                }

                return Unauthorized();
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

        [Authorize(Roles = "Admin,User")]
        [HttpGet("getshoppingcartbyid/{shoppingCartId}")]
        public async Task<IActionResult> GetShoppingCartByIdAsync([FromRoute] Guid shoppingCartId)
        {
            try
            {
                if (HttpContext.User.Identity != null && HttpContext.User.Identity.Name != null)
                {
                    var user = await _userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
                    if (user != null)
                    {
                        var admins = await _userManager.GetUsersInRoleAsync("Admin");
                        var shoppingCart = await _shoppingCartRepository.GetShoppingCartByIdAsync(shoppingCartId);
                        if (user.Id == shoppingCart.UserId || admins.Contains(user))
                        {
                            var response = await _shoppingCartService.GetShoppingCartByIdAsync(shoppingCartId);

                            return Ok(response);
                        }
                    }
                }
                return Unauthorized();
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

        [Authorize(Roles = "Admin")]
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
