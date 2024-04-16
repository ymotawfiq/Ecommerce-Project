using Ecommerce.Data.DTOs;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;

using Ecommerce.Service.Services.PromotionCategoryService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [ApiController]
    public class PromotionCategoryController : ControllerBase
    {
        private readonly IPromotionCategoryService _promotionCategoryService;
        public PromotionCategoryController
            (
            IPromotionCategoryService _promotionCategoryService
            )
        {
            this._promotionCategoryService = _promotionCategoryService;
        }

        [AllowAnonymous]
        [HttpGet("promotionCategorirs")]
        public async Task<IActionResult> GetAllPromotionCategoriesAsync()
        {
            try
            {
                var response = await _promotionCategoryService.GetAllPromotionCategoriesAsync();
                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<IEnumerable<PromotionCategory>>
                    {
                        StatusCode = 500,
                        IsSuccess = true,
                        Message = ex.Message,
                        ResponseObject = new List<PromotionCategory>()
                    });
            }
        }

        [AllowAnonymous]
        [HttpGet("promotionCategoryByCategory/{categoryId}")]
        public async Task<IActionResult> GetAllPromotionCategoriesByCategoryIdAsync([FromRoute] Guid categoryId)
        {
            try
            {
                var response = await _promotionCategoryService.GetAllPromotionCategoriesByCategoryIdAsync(categoryId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<IEnumerable<PromotionCategory>>
                    {
                        StatusCode = 500,
                        IsSuccess = true,
                        Message = ex.Message,
                        ResponseObject = new List<PromotionCategory>()
                    });
            }
        }

        [AllowAnonymous]
        [HttpGet("promotionCategoryByPromotion/{promotionId}")]
        public async Task<IActionResult> GetAllPromotionCategoriesByPromotionIdAsync
            ([FromRoute] Guid promotionId)
        {
            try
            {
                var response = await _promotionCategoryService.GetAllPromotionCategoriesByPromotionIdAsync(promotionId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<IEnumerable<PromotionCategory>>
                    {
                        StatusCode = 500,
                        IsSuccess = true,
                        Message = ex.Message,
                        ResponseObject = new List<PromotionCategory>()
                    });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("addPromotionCategory")]
        public async Task<IActionResult> AddPromotionCategoryAsync
            ([FromBody] PromotionCategoryDto promotionCategoryDto)
        {
            try
            {
                var response = await _promotionCategoryService.AddPromotionCategoryAsync(promotionCategoryDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<PromotionCategory>
                    {
                        StatusCode = 500,
                        IsSuccess = true,
                        Message = ex.Message,
                        ResponseObject = new PromotionCategory()
                    });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("updatePromotionCategory")]
        public async Task<IActionResult> UpdatePromotionCategoryAsync
            ([FromBody] PromotionCategoryDto promotionCategoryDto)
        {
            try
            {
                var response = await _promotionCategoryService.UpdatePromotionCategoryAsync(promotionCategoryDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<PromotionCategory>
                    {
                        StatusCode = 500,
                        IsSuccess = true,
                        Message = ex.Message,
                        ResponseObject = new PromotionCategory()
                    });
            }
        }

        [AllowAnonymous]
        [HttpGet("promotionCategory/{promotionCategoryId}")]
        public async Task<IActionResult> GetPromotionCategoryByIdAsync([FromRoute] Guid promotionCategoryId)
        {
            try
            {
                var response = await _promotionCategoryService.GetPromotionCategoryByIdAsync(promotionCategoryId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<PromotionCategory>
                    {
                        StatusCode = 500,
                        IsSuccess = true,
                        Message = ex.Message,
                        ResponseObject = new PromotionCategory()
                    });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("deletePromotionCategory/{promotionCategoryId}")]
        public async Task<IActionResult> DeletePromotionCategoryByIdAsync([FromRoute] Guid promotionCategoryId)
        {
            try
            {
                var response = await _promotionCategoryService.DeletePromotionCategoryByIdAsync(promotionCategoryId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<PromotionCategory>
                    {
                        StatusCode = 500,
                        IsSuccess = true,
                        Message = ex.Message,
                        ResponseObject = new PromotionCategory()
                    });
            }
        }


    }
}
