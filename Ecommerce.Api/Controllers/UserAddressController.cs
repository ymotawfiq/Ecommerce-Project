﻿using Ecommerce.Data.DTOs;
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
    [Route("api/[controller]")]
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
        [HttpGet("allusersaddresses")]
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
        [HttpGet("getuseraddressesbyuserid/{userId}")]
        public async Task<IActionResult> GetUserAddressesByUserIdAsync([FromRoute] Guid userId)
        {
            try
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                if (user != null)
                {
                    var adminUsers = await _userManager.GetUsersInRoleAsync("Admin");
                    if(user.Id == userId.ToString() || adminUsers.Contains(user))
                    {
                        var response = await _userAddressService.GetUserAddressesByUserIdAsync(userId);
                        return Ok(response);
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
        [HttpGet("getuseraddressesbyusernameoremail")]
        public async Task<IActionResult> GetUserAddressesByUsernameOrEmailAsync
            (string userNameOrEmail)
        {
            try
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
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
        [HttpPost("adduseraddress")]
        public async Task<IActionResult> AddUserAddressAsync([FromBody] UserAddressDto userAddressDto)
        {
            try
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
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
        [HttpPut("updateuseraddress")]
        public async Task<IActionResult> UpdateUserAddressAsync([FromBody] UserAddressDto userAddressDto)
        {
            try
            {

                var user = await _userManager.GetUserAsync(HttpContext.User);
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
        [HttpGet("getuseraddressbyid/{userAddressId}")]
        public async Task<IActionResult> GetUserAddressByIdAsync([FromRoute] Guid userAddressId)
        {
            try
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
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
        [HttpDelete("deleteuseraddressbyid/{userAddressId}")]
        public async Task<IActionResult> DeleteUserAddressByIdAsync([FromRoute] Guid userAddressId)
        {
            try
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
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
