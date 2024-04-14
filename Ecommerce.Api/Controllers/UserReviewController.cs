using Ecommerce.Data.DTOs;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Data.Models.Entities.Authentication;
using Ecommerce.Repository.Repositories.UserReviewRepository;
using Ecommerce.Service.Services.UserReviewService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserReviewController : ControllerBase
    {
        private readonly IUserReviewService _userReviewService;
        private readonly UserManager<SiteUser> _userManager;
        private readonly IUserReview _userReviewRepository;
        public UserReviewController(IUserReviewService _userReviewService, UserManager<SiteUser> _userManager,
            IUserReview _userReviewRepository)
        {
            this._userReviewService = _userReviewService;
            this._userManager = _userManager;
            this._userReviewRepository = _userReviewRepository;
        }

        [Authorize(Roles = "Admin")]
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

        [AllowAnonymous]
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

        [AllowAnonymous]
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

        [Authorize(Roles = "Admin,User")]
        [HttpPost("adduserreview")]
        public async Task<IActionResult> AddUserReviewAsync([FromBody] UserReviewDto userReviewDto)
        {
            try
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                if (user != null)
                {
                    var admins = await _userManager.GetUsersInRoleAsync("Admin");
                    if(userReviewDto.UsernameOrEmail==user.Email 
                        || userReviewDto.UsernameOrEmail == user.UserName || admins.Contains(user))
                    {
                        var response = await _userReviewService.AddUserReviewAsync(userReviewDto);
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
        [HttpPut("updateuserreview")]
        public async Task<IActionResult> UpdateUserReviewAsync([FromBody] UserReviewDto userReviewDto)
        {
            try
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                if (user != null)
                {
                    var admins = await _userManager.GetUsersInRoleAsync("Admin");
                    if (userReviewDto.UsernameOrEmail == user.Email
                        || userReviewDto.UsernameOrEmail == user.UserName || admins.Contains(user))
                    {
                        var response = await _userReviewService.UpdateUserReviewAsync(userReviewDto);
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

        [AllowAnonymous]
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

        [Authorize(Roles = "Admin,User")]
        [HttpDelete("deleteuserreviewbyid/{userReviewId}")]
        public async Task<IActionResult> DeleteUserReviewByIdAsync([FromRoute] Guid userReviewId)
        {
            try
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                if (user != null)
                {
                    UserReview userReview = await _userReviewRepository.GetUserReviewByIdAsync(userReviewId);
                    var admins = await _userManager.GetUsersInRoleAsync("Admin");
                    if (userReview.UserId == user.Id || admins.Contains(user))
                    {
                        var response = await _userReviewService.DeleteUserReviewByIdAsync(userReviewId);
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
