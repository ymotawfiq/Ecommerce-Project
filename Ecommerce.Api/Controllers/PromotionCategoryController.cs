using Ecommerce.Data.DTOs;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;

using Ecommerce.Service.Services.PromotionCategoryService;
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


        [HttpGet("allpromotioncategory")]
        public async Task<IActionResult> GetAllPromotionCategoriesAsync()
        {
            try
            {
                var response = await _promotionCategoryService.GetAllPromotionCategoriesAsync();
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<IEnumerable<PromotionCategory>>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Unknown error",
                        ResponseObject = new List<PromotionCategory>()
                    });
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


        [HttpGet("allpromotioncategorybycategoryid/{categoryId}")]
        public async Task<IActionResult> GetAllPromotionCategoriesByCategoryIdAsync([FromRoute] Guid categoryId)
        {
            try
            {
                var response = await _promotionCategoryService.GetAllPromotionCategoriesByCategoryIdAsync(categoryId);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<IEnumerable<PromotionCategory>>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Unknown error",
                        ResponseObject = new List<PromotionCategory>()
                    });
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

        [HttpGet("allpromotioncategorybypromotionid/{promotionId}")]
        public async Task<IActionResult> GetAllPromotionCategoriesByPromotionIdAsync
            ([FromRoute] Guid promotionId)
        {
            try
            {
                var response = await _promotionCategoryService.GetAllPromotionCategoriesByPromotionIdAsync(promotionId);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<IEnumerable<PromotionCategory>>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Unknown error",
                        ResponseObject = new List<PromotionCategory>()
                    });
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

        [HttpPost("addpromotioncategory")]
        public async Task<IActionResult> AddPromotionCategoryAsync
            ([FromBody] PromotionCategoryDto promotionCategoryDto)
        {
            try
            {
                var response = await _promotionCategoryService.AddPromotionCategoryAsync(promotionCategoryDto);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<PromotionCategory>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Unknown error",
                        ResponseObject = new PromotionCategory()
                    });
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

        [HttpPut("updatepromotioncategory")]
        public async Task<IActionResult> UpdatePromotionCategoryAsync
            ([FromBody] PromotionCategoryDto promotionCategoryDto)
        {
            try
            {
                var response = await _promotionCategoryService.UpdatePromotionCategoryAsync(promotionCategoryDto);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<PromotionCategory>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Unknown error",
                        ResponseObject = new PromotionCategory()
                    });
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


        [HttpGet("getpromotioncategorybyid/{promotionCategoryId}")]
        public async Task<IActionResult> GetPromotionCategoryByIdAsync([FromRoute] Guid promotionCategoryId)
        {
            try
            {
                var response = await _promotionCategoryService.GetPromotionCategoryByIdAsync(promotionCategoryId);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<PromotionCategory>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Unknown error",
                        ResponseObject = new PromotionCategory()
                    });
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


        [HttpDelete("deletepromotioncategorybyid/{promotionCategoryId}")]
        public async Task<IActionResult> DeletePromotionCategoryByIdAsync([FromRoute] Guid promotionCategoryId)
        {
            try
            {
                var response = await _promotionCategoryService.DeletePromotionCategoryByIdAsync(promotionCategoryId);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<PromotionCategory>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Unknown error",
                        ResponseObject = new PromotionCategory()
                    });
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
