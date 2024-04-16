using Ecommerce.Data.DTOs;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities.Authentication;
using Ecommerce.Service.Services.ShopOrderService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [ApiController]
    public class ShopOrderController : ControllerBase
    {
        private readonly IShopOrderService _shopOrderService;
        private readonly UserManager<SiteUser> _userManager;
        public ShopOrderController(IShopOrderService _shopOrderService, UserManager<SiteUser> _userManager)
        {
            this._shopOrderService = _shopOrderService;
            this._userManager = _userManager;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("shoppingOrders")]
        public async Task<IActionResult> GetAllShopOrdersAsync()
        {
            try
            {
                var response = await _shopOrderService.GetAllShopOrdersAsync();
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
        [HttpGet("shoppingOrdersByUsernameOrEmail")]
        public async Task<IActionResult> GetAllShopOrdersByUserUsernameOrEmailAsync(string usernameOrEmail)
        {
            try
            {
                if(HttpContext.User.Identity!=null && HttpContext.User.Identity.Name != null)
                {
                    var user = await _userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
                    if (user != null)
                    {
                        var admins = await _userManager.GetUsersInRoleAsync("Admin");
                        if (user.Email == usernameOrEmail || user.UserName == usernameOrEmail
                            || admins.Contains(user))
                        {
                            var response = await _shopOrderService.GetAllShopOrdersByUserUsernameOrEmailAsync(usernameOrEmail);
                            return Ok(response);
                        }
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
        [HttpGet("shoppingOrdersByDate")]
        public async Task<IActionResult> GetAllShopOrdersByDateAsync(DateTime date)
        {
            try
            {
                var response = await _shopOrderService.GetAllShopOrdersByDateAsync(date);
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

        [Authorize(Roles = "Admin")]
        [HttpGet("shoppingOrdersByShippingMethod/{shippingMethodId}")]
        public async Task<IActionResult> GetAllShopOrdersByShippingMethodIdAsync(Guid shippingMethodId)
        {
            try
            {
                var response = await _shopOrderService.GetAllShopOrdersByShippingMethodIdAsync(shippingMethodId);
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

        [Authorize(Roles = "Admin")]
        [HttpGet("shoppingOrdersByAddress/{addressId}")]
        public async Task<IActionResult> GetAllShopOrdersByAddressIdAsync(Guid addressId)
        {
            try
            {
                var response = await _shopOrderService.GetAllShopOrdersByAddressIdAsync(addressId);
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

        [Authorize(Roles = "Admin")]
        [HttpGet("shoppingOrdersByPaymentMethod/{paymentMethodId}")]
        public async Task<IActionResult> GetAllShopOrdersByPaymentIdAsync(Guid paymentMethodId)
        {
            try
            {
                var response = await _shopOrderService.GetAllShopOrdersByPaymentMethodIdAsync(paymentMethodId);
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

        [Authorize(Roles = "Admin,User")]
        [HttpPost("addShopOrder")]
        public async Task<IActionResult> AddShopOrderAsync([FromBody] ShopOrderDto shopOrderDto)
        {
            try
            {
                var response = await _shopOrderService.AddShopOrderAsync(shopOrderDto);
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

        [Authorize(Roles = "Admin,User")]
        [HttpPut("updateShopOrder")]
        public async Task<IActionResult> UpdateShopOrderAsync([FromBody] ShopOrderDto shopOrderDto)
        {
            try
            {

                if (HttpContext.User.Identity != null && HttpContext.User.Identity.Name != null)
                {
                    var user = await _userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
                    if (user != null)
                    {
                        var admins = await _userManager.GetUsersInRoleAsync("Admin");
                        if (user.Email == shopOrderDto.UsernameOrEmail || user.UserName == shopOrderDto.UsernameOrEmail
                            || admins.Contains(user))
                        {
                            var response = await _shopOrderService.UpdateShopOrderAsync(shopOrderDto);
                            return Ok(response);
                        }
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
        [HttpGet("shopOrder/{shopOrderId}")]
        public async Task<IActionResult> GetShopOrderByIdAsync([FromRoute] Guid shopOrderId)
        {
            try
            {
                var response = await _shopOrderService.GetShopOrderByIdAsync(shopOrderId);
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

        [Authorize(Roles = "Admin")]
        [HttpDelete("deleteShopOrder/{shopOrderId}")]
        public async Task<IActionResult> DeleteShopOrder([FromRoute] Guid shopOrderId)
        {
            try
            {
                var response = await _shopOrderService.DeleteShopOrderByIdAsync(shopOrderId);
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
