using Ecommerce.Data.DTOs;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;

using Ecommerce.Service.Services.PromotionCategoryService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
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
        [HttpGet("allpromotioncategory")]
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
        [HttpGet("allpromotioncategorybycategoryid/{categoryId}")]
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
        [HttpGet("allpromotioncategorybypromotionid/{promotionId}")]
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
        [HttpPost("addpromotioncategory")]
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
        [HttpPut("updatepromotioncategory")]
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
        [HttpGet("getpromotioncategorybyid/{promotionCategoryId}")]
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
        [HttpDelete("deletepromotioncategorybyid/{promotionCategoryId}")]
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
