using Ecommerce.Data.DTOs;
using Ecommerce.Data.Extensions;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;

using Ecommerce.Service.Services.UserAddressService;

using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAddressController : ControllerBase
    {

        private readonly IUserAddressService _userAddressService;
        public UserAddressController(IUserAddressService _userAddressService)
        {
            this._userAddressService = _userAddressService;
        }

        [HttpGet("allusersaddresses")]
        public async Task<IActionResult> GetAllUserAddressAsync()
        {
            try
            {
                var response = await _userAddressService.GetAllUserAddressAsync();
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<UserAddress>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Failed to get users addresses"
                });
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


        [HttpGet("getuseraddressesbyuserid/{userId}")]
        public async Task<IActionResult> GetUserAddressesByUserIdAsync([FromRoute] Guid userId)
        {
            try
            {
                var response = await _userAddressService.GetUserAddressesByUserIdAsync(userId);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<UserAddress>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Failed to get users addresses for this user"
                });
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



        [HttpPost("getuseraddressesbyusernameoremail")]
        public async Task<IActionResult> GetUserAddressesByUsernameOrEmailAsync
            (string userNameOrEmail)
        {
            try
            {
                var response = await _userAddressService.GetUserAddressesByUsernameOrEmailAsync(userNameOrEmail);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<UserAddress>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Failed to get users addresses for this user"
                });
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

        [HttpPost("adduseraddress")]
        public async Task<IActionResult> AddUserAddressAsync([FromBody] UserAddressDto userAddressDto)
        {
            try
            {
                var response = await _userAddressService.AddUserAddressAsync(userAddressDto);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<UserAddress>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Failed to add user addresse"
                });
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


        [HttpPut("updateuseraddress")]
        public async Task<IActionResult> UpdateUserAddressAsync([FromBody] UserAddressDto userAddressDto)
        {
            try
            {
                var response = await _userAddressService.UpdateUserAddressAsync(userAddressDto);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<UserAddress>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Failed to update user addresse"
                });
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

        [HttpGet("getuseraddressbyid/{userAddressId}")]
        public async Task<IActionResult> GetUserAddressByIdAsync([FromRoute] Guid userAddressId)
        {
            try
            {
                var response = await _userAddressService.GetUserAddressByIdAsync(userAddressId);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<UserAddress>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Failed to find user addresse"
                });
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

        [HttpDelete("deleteuseraddressbyid/{userAddressId}")]
        public async Task<IActionResult> DeleteUserAddressByIdAsync([FromRoute] Guid userAddressId)
        {
            try
            {
                var response = await _userAddressService.DeleteUserAddressByIdAsync(userAddressId);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<UserAddress>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Failed to delete user addresse"
                });
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
