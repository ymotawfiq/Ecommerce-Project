using Ecommerce.Data.DTOs;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Data.Models.Entities.Authentication;
using Ecommerce.Service.Services.UserPaymentMethodService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [ApiController]
    public class UserPaymentMethodController : ControllerBase
    {
        private readonly IUserPaymentMethodService _userPaymentMethodService;
        private readonly UserManager<SiteUser> _userManager;
        public UserPaymentMethodController(IUserPaymentMethodService _userPaymentMethodService,
            UserManager<SiteUser> _userManager)
        {
            this._userPaymentMethodService = _userPaymentMethodService;
            this._userManager = _userManager;
        }

        [AllowAnonymous]
        [HttpGet("userPaymentMethods")]
        public async Task<IActionResult> GetAllUsersPaymentsMethodsAsync()
        {
            try
            {
                var response = await _userPaymentMethodService.GetAllUsersPaymentMethodsAsync();
                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<IEnumerable<UserPaymentMethod>>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message
                    });
            }
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet("userPaymentMethods/{userId}")]
        public async Task<IActionResult> GetAllUsersPaymentsMethodsByUserIdAsync([FromRoute] string userId)
        {
            try
            {
                if (HttpContext.User.Identity != null && HttpContext.User.Identity.Name != null)
                {
                    var user = await _userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
                    if (user != null)
                    {
                        var admins = await _userManager.GetUsersInRoleAsync("Admin");
                        if (admins.Contains(user) || user.Id == userId)
                        {
                            var response = await _userPaymentMethodService.GetAllUsersPaymentMethodsAsyncByUserIdAsync(userId);
                            return Ok(response);
                        }
                    }
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<IEnumerable<UserPaymentMethod>>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message
                    });
            }
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet("userPaymentMethodsByUsername")]
        public async Task<IActionResult> GetAllUsersPaymentsMethodsByUserUsernameOrEmailAsync
            (string userNameOrEmail)
        {
            try
            {
                if (HttpContext.User.Identity != null && HttpContext.User.Identity.Name != null)
                {
                    var user = await _userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
                    if (user != null)
                    {
                        var admins = await _userManager.GetUsersInRoleAsync("Admin");
                        if (admins.Contains(user) || user.Email == userNameOrEmail
                            || user.UserName == userNameOrEmail)
                        {
                            var response = await _userPaymentMethodService
                                .GetAllUsersPaymentMethodsAsyncByUserUsernameOrEmailAsync(userNameOrEmail);
                            return Ok(response);
                        }
                    }
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<IEnumerable<UserPaymentMethod>>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message
                    });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("addUserPaymentMethod")]
        public async Task<IActionResult> AddUserPaymentMethodAsync
            ([FromBody] UserPaymentMethodDto userPaymentMethodDto)
        {
            //var response = await _userPaymentMethodService.AddUserPaymentMethodAsync(userPaymentMethodDto);
            //return Ok(response);
            try
            {
                var response = await _userPaymentMethodService.AddUserPaymentMethodAsync(userPaymentMethodDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<UserPaymentMethod>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message
                    });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("updateUserPaymentMethod")]
        public async Task<IActionResult> UpdateUserPaymentMethodAsync([FromBody] UserPaymentMethodDto userPaymentMethodDto)
        {
            try
            {
                var response = await _userPaymentMethodService.UpdateUserPaymentMethodAsync(userPaymentMethodDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<UserPaymentMethod>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message
                    });
            }
        }

        [AllowAnonymous]
        [HttpGet("userPaymentMethod/{userMethodId}")]
        public async Task<IActionResult> GetUserPaymentMethodAsync([FromRoute] Guid userMethodId)
        {
            try
            {
                var response = await _userPaymentMethodService.GetUserPaymentMethodByIdAsync(userMethodId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<UserPaymentMethod>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message
                    });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("deleteUserPaymentMethod/{userMethodId}")]
        public async Task<IActionResult> DeleteUserPaymentMethodAsync([FromRoute] Guid userMethodId)
        {
            try
            {
                var response = await _userPaymentMethodService.DeleteUserPaymentMethodByIdAsync(userMethodId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<UserPaymentMethod>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message
                    });
            }
        }



    }
}
