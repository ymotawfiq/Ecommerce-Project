using Ecommerce.Data.DTOs;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Service.Services.UserPaymentMethodService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPaymentMethodController : ControllerBase
    {
        private readonly IUserPaymentMethodService _userPaymentMethodService;
        public UserPaymentMethodController(IUserPaymentMethodService _userPaymentMethodService)
        {
            this._userPaymentMethodService = _userPaymentMethodService;
        }


        [HttpGet("alluserpaymentmethods")]
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

        [HttpGet("alluserpaymentmethodsbyuserid/{userId}")]
        public async Task<IActionResult> GetAllUsersPaymentsMethodsByUserIdAsync([FromRoute] string userId)
        {
            try
            {
                var response = await _userPaymentMethodService.GetAllUsersPaymentMethodsAsyncByUserIdAsync(userId);
                return Ok(response);
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

        [HttpGet("alluserpaymentmethodsbyuserusernameoremail")]
        public async Task<IActionResult> GetAllUsersPaymentsMethodsByUserUsernameOrEmailAsync
            (string userNameOrEmail)
        {
            try
            {
                var response = await _userPaymentMethodService
                    .GetAllUsersPaymentMethodsAsyncByUserUsernameOrEmailAsync(userNameOrEmail);
                return Ok(response);
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

        [HttpPost("adduserpaymentmethod")]
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


        [HttpPut("updateuserpaymentmethod")]
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

        [HttpGet("getuserpaymentmethodbyid/{userMethodId}")]
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

        [HttpDelete("deleteuserpaymentmethodbyid/{userMethodId}")]
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
