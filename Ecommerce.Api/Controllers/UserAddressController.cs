using Ecommerce.Data.DTOs;
using Ecommerce.Data.Extensions;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Data.Models.Entities.Authentication;
using Ecommerce.Repository.Repositories.AddressRepository;
using Ecommerce.Repository.Repositories.UserAddressRepository;
using Ecommerce.Service.Services.UserAddressService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Authorize(Roles ="Admin,User")]
    [ApiController]
    public class UserAddressController : ControllerBase
    {

        private readonly IUserAddressService _userAddressService;
        private readonly UserManager<SiteUser> _userManager;
        private readonly IUserAddress _userAddressRepository;
        public UserAddressController(IUserAddressService _userAddressService
            , UserManager<SiteUser> _userManager, IUserAddress _userAddressRepository)
        {
            this._userAddressService = _userAddressService;
            this._userManager = _userManager;
            this._userAddressRepository = _userAddressRepository;
        }

        [Authorize(Roles ="Admin")]
        [HttpGet("users-addresses")]
        public async Task<IActionResult> GetAllUserAddressAsync()
        {
            try
            {
                var response = await _userAddressService.GetAllUserAddressAsync();
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
        [HttpGet("userAddresses/{userId}")]
        public async Task<IActionResult> GetUserAddressesByUserIdAsync([FromRoute] Guid userId)
        {
            try
            {
                if (HttpContext.User.Identity != null && HttpContext.User.Identity.Name != null)
                {
                    var user = await _userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
                    if (user != null)
                    {
                        var adminUsers = await _userManager.GetUsersInRoleAsync("Admin");
                        if (user.Id == userId.ToString() || adminUsers.Contains(user))
                        {
                            var response = await _userAddressService.GetUserAddressesByUserIdAsync(userId);
                            return Ok(response);
                        }
                    }
                }
                return Unauthorized();
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
        [HttpGet("userAddresses")]
        public async Task<IActionResult> GetUserAddressesByUsernameOrEmailAsync
            (string userNameOrEmail)
        {
            try
            {
                if (HttpContext.User.Identity != null && HttpContext.User.Identity.Name != null)
                {
                    var user = await _userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
                    if (user != null)
                    {
                        var adminUsers = await _userManager.GetUsersInRoleAsync("Admin");
                        if (user.Email == userNameOrEmail || adminUsers.Contains(user))
                        {
                            var response = await _userAddressService
                                .GetUserAddressesByUsernameOrEmailAsync(userNameOrEmail);
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
        [HttpPost("addUserAddress")]
        public async Task<IActionResult> AddUserAddressAsync([FromBody] UserAddressDto userAddressDto)
        {
            try
            {
                if (HttpContext.User.Identity != null && HttpContext.User.Identity.Name != null)
                {
                    var user = await _userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
                    if (user != null)
                    {
                        var adminUsers = await _userManager.GetUsersInRoleAsync("Admin");
                        if (user.Email == userAddressDto.UserIdOrEmail
                            || user.Id == userAddressDto.UserIdOrEmail || adminUsers.Contains(user))
                        {
                            var response = await _userAddressService.AddUserAddressAsync(userAddressDto);
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
        [HttpPut("updateUserAddress")]
        public async Task<IActionResult> UpdateUserAddressAsync([FromBody] UserAddressDto userAddressDto)
        {
            try
            {

                if (HttpContext.User.Identity != null && HttpContext.User.Identity.Name != null)
                {
                    var user = await _userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
                    if (user != null)
                    {
                        var adminUsers = await _userManager.GetUsersInRoleAsync("Admin");
                        if (user.Email == userAddressDto.UserIdOrEmail
                            || user.Id == userAddressDto.UserIdOrEmail || adminUsers.Contains(user))
                        {
                            var response = await _userAddressService.UpdateUserAddressAsync(userAddressDto);
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
        [HttpGet("userAddress/{userAddressId}")]
        public async Task<IActionResult> GetUserAddressByIdAsync([FromRoute] Guid userAddressId)
        {
            try
            {
                if (HttpContext.User.Identity != null && HttpContext.User.Identity.Name != null)
                {
                    var user = await _userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
                    var userAddress = await _userAddressRepository.GetUserAddressByIdAsync(userAddressId);
                    if (user != null && userAddress != null)
                    {
                        var adminUsers = await _userManager.GetUsersInRoleAsync("Admin");
                        if (user.Id == userAddress.UserId
                            || adminUsers.Contains(user))
                        {
                            var response = await _userAddressService.GetUserAddressByIdAsync(userAddressId);
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
        [HttpDelete("deleteUserAddress/{userAddressId}")]
        public async Task<IActionResult> DeleteUserAddressByIdAsync([FromRoute] Guid userAddressId)
        {
            try
            {
                if (HttpContext.User.Identity != null && HttpContext.User.Identity.Name != null)
                {
                    var user = await _userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
                    var userAddress = await _userAddressRepository.GetUserAddressByIdAsync(userAddressId);
                    if (user != null && userAddress != null)
                    {
                        var adminUsers = await _userManager.GetUsersInRoleAsync("Admin");
                        if (user.Id == userAddress.UserId
                            || adminUsers.Contains(user))
                        {
                            var response = await _userAddressService.DeleteUserAddressByIdAsync(userAddressId);
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


    }
}
