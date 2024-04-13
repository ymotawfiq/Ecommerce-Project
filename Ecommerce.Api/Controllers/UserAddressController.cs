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


        [HttpGet("getuseraddressesbyuserid/{userId}")]
        public async Task<IActionResult> GetUserAddressesByUserIdAsync([FromRoute] Guid userId)
        {
            try
            {
                var response = await _userAddressService.GetUserAddressesByUserIdAsync(userId);
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



        [HttpGet("getuseraddressesbyusernameoremail")]
        public async Task<IActionResult> GetUserAddressesByUsernameOrEmailAsync
            (string userNameOrEmail)
        {
            try
            {
                var response = await _userAddressService.GetUserAddressesByUsernameOrEmailAsync(userNameOrEmail);
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

        [HttpPost("adduseraddress")]
        public async Task<IActionResult> AddUserAddressAsync([FromBody] UserAddressDto userAddressDto)
        {
            try
            {
                var response = await _userAddressService.AddUserAddressAsync(userAddressDto);
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


        [HttpPut("updateuseraddress")]
        public async Task<IActionResult> UpdateUserAddressAsync([FromBody] UserAddressDto userAddressDto)
        {
            try
            {
                var response = await _userAddressService.UpdateUserAddressAsync(userAddressDto);
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

        [HttpGet("getuseraddressbyid/{userAddressId}")]
        public async Task<IActionResult> GetUserAddressByIdAsync([FromRoute] Guid userAddressId)
        {
            try
            {
                var response = await _userAddressService.GetUserAddressByIdAsync(userAddressId);
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

        [HttpDelete("deleteuseraddressbyid/{userAddressId}")]
        public async Task<IActionResult> DeleteUserAddressByIdAsync([FromRoute] Guid userAddressId)
        {
            try
            {
                var response = await _userAddressService.DeleteUserAddressByIdAsync(userAddressId);
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
