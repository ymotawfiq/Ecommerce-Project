using Ecommerce.Data.DTOs;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Service.Services.PromotionService;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Admin")]
        [HttpGet("allpromotions")]
        public async Task<IActionResult> GetAllPromotionsAsync()
        {
            try
            {
                var response = await _promotionService.GetAllPromotionsAsync();
                return Ok(response);
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

        [Authorize(Roles = "Admin")]
        [HttpPost("addpromotion")]
        public async Task<IActionResult> AddPromotionAsync([FromBody] PromotionDto promotionDto)
        {
            try
            {
                var response = await _promotionService.AddPromotionAsync(promotionDto);
                return Ok(response);
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

        [Authorize(Roles = "Admin")]
        [HttpPut("Updatepromotion")]
        public async Task<IActionResult> UpdatePromotionAsync([FromBody] PromotionDto promotionDto)
        {
            try
            {
                var response = await _promotionService.UpdatePromotionAsync(promotionDto);
                return Ok(response);
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

        [AllowAnonymous]
        [HttpGet("getpromotionbyid/{promotionId}")]
        public async Task<IActionResult> GetPromotionByIdAsync([FromRoute] Guid promotionId)
        {
            try
            {
                var response = await _promotionService.GetPromotionByIdAsync(promotionId);
                return Ok(response);
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

        [Authorize(Roles = "Admin")]
        [HttpDelete("deletepromotionbyid/{promotionId}")]
        public async Task<IActionResult> DeletePromotionByIdAsync([FromRoute] Guid promotionId)
        {
            try
            {
                var response = await _promotionService.DeletePromotionByIdAsync(promotionId);
                return Ok(response);
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
