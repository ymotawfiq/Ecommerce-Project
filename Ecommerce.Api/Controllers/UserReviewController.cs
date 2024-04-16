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
        [HttpGet("reviews")]
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
        [HttpGet("reviewsByOrder/{orderLineId}")]
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
        [HttpGet("reviewsByUsernameOrEmail")]
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
        [HttpPost("addUserReview")]
        public async Task<IActionResult> AddUserReviewAsync([FromBody] UserReviewDto userReviewDto)
        {
            try
            {
                if (HttpContext.User.Identity != null && HttpContext.User.Identity.Name != null)
                {
                    var user = await _userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
                    if (user != null)
                    {
                        var admins = await _userManager.GetUsersInRoleAsync("Admin");
                        if (userReviewDto.UsernameOrEmail == user.Email
                            || userReviewDto.UsernameOrEmail == user.UserName || admins.Contains(user))
                        {
                            var response = await _userReviewService.AddUserReviewAsync(userReviewDto);
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
        [HttpPut("updateUserReview")]
        public async Task<IActionResult> UpdateUserReviewAsync([FromBody] UserReviewDto userReviewDto)
        {
            try
            {
                if (HttpContext.User.Identity != null && HttpContext.User.Identity.Name != null)
                {
                    var user = await _userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
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
        [HttpGet("userReview/{userReviewId}")]
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
        [HttpDelete("deleteUserReview/{userReviewId}")]
        public async Task<IActionResult> DeleteUserReviewByIdAsync([FromRoute] Guid userReviewId)
        {
            try
            {
                if (HttpContext.User.Identity != null && HttpContext.User.Identity.Name != null)
                {
                    var user = await _userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
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
