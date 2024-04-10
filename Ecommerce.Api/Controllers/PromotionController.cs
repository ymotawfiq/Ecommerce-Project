using Ecommerce.Data.DTOs;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Service.Services.PromotionService;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionService _promotionService;
        public PromotionController(IPromotionService _promotionService)
        {
            this._promotionService = _promotionService;
        }

        [HttpGet("allpromotions")]
        public async Task<IActionResult> GetAllPromotionsAsync()
        {
            try
            {
                var response = await _promotionService.GetAllPromotionsAsync();
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<IEnumerable<Promotion>>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Unknown error",
                        ResponseObject = new List<Promotion>()
                    });
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<IEnumerable<Promotion>>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message,
                        ResponseObject = new List<Promotion>()
                    });
            }
        }


        [HttpPost("addpromotion")]
        public async Task<IActionResult> AddPromotionAsync([FromBody] PromotionDto promotionDto)
        {
            try
            {
                var response = await _promotionService.AddPromotionAsync(promotionDto);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<Promotion>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Unknown error",
                        ResponseObject = new Promotion()
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<Promotion>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message,
                        ResponseObject = new Promotion()
                    });
            }
        }


        [HttpPut("Updatepromotion")]
        public async Task<IActionResult> UpdatePromotionAsync([FromBody] PromotionDto promotionDto)
        {
            try
            {
                var response = await _promotionService.UpdatePromotionAsync(promotionDto);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<Promotion>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Unknown error",
                        ResponseObject = new Promotion()
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<Promotion>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message,
                        ResponseObject = new Promotion()
                    });
            }
        }

        [HttpGet("getpromotionbyid/{promotionId}")]
        public async Task<IActionResult> GetPromotionByIdAsync([FromRoute] Guid promotionId)
        {
            try
            {
                var response = await _promotionService.GetPromotionByIdAsync(promotionId);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<Promotion>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Unknown error",
                        ResponseObject = new Promotion()
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<Promotion>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message,
                        ResponseObject = new Promotion()
                    });
            }
        }


        [HttpDelete("deletepromotionbyid/{promotionId}")]
        public async Task<IActionResult> DeletePromotionByIdAsync([FromRoute] Guid promotionId)
        {
            try
            {
                var response = await _promotionService.DeletePromotionByIdAsync(promotionId);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<Promotion>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Unknown error",
                        ResponseObject = new Promotion()
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<Promotion>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message,
                        ResponseObject = new Promotion()
                    });
            }
        }

    }
}
