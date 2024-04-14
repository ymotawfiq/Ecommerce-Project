using Ecommerce.Data.DTOs;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Service.Services.UserReviewService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserReviewController : ControllerBase
    {
        private readonly IUserReviewService _userReviewService;
        public UserReviewController(IUserReviewService _userReviewService)
        {
            this._userReviewService = _userReviewService;
        }

        [HttpGet("allreviews")]
        public async Task<IActionResult> GetAllReviewsAsync()
        {
            try
            {
                var response = await _userReviewService.GetAllUserReviewsAsync();
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

        [HttpGet("allreviewsbyorderid/{orderLineId}")]
        public async Task<IActionResult> GetAllReviewsByOrderLineIdAsync([FromRoute] Guid orderLineId)
        {
            try
            {
                var response = await _userReviewService.GetAllUserReviewsByOrderLineIdAsync(orderLineId);
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


        [HttpGet("allreviewsbyusernameoremail")]
        public async Task<IActionResult> GetAllReviewsByUsernameOrEmailAsync(string usernameOrEmail)
        {
            try
            {
                var response = await _userReviewService.GetAllUserReviewsByUserUsernameOrEmailAsync(usernameOrEmail);
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

        [HttpPost("adduserreview")]
        public async Task<IActionResult> AddUserReviewAsync([FromBody] UserReviewDto userReviewDto)
        {
            try
            {
                var response = await _userReviewService.AddUserReviewAsync(userReviewDto);
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

        [HttpPut("updateuserreview")]
        public async Task<IActionResult> UpdateUserReviewAsync([FromBody] UserReviewDto userReviewDto)
        {
            try
            {
                var response = await _userReviewService.UpdateUserReviewAsync(userReviewDto);
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

        [HttpGet("getuserreviewbyid/{userReviewId}")]
        public async Task<IActionResult> GetUserReviewByIdAsync([FromRoute] Guid userReviewId)
        {
            try
            {
                var response = await _userReviewService.GetUserReviewByIdAsync(userReviewId);
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

        [HttpDelete("deleteuserreviewbyid/{userReviewId}")]
        public async Task<IActionResult> DeleteUserReviewByIdAsync([FromRoute] Guid userReviewId)
        {
            try
            {
                var response = await _userReviewService.DeleteUserReviewByIdAsync(userReviewId);
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
