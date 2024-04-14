using Ecommerce.Data.DTOs;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;

using Ecommerce.Service.Services.VariationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VariationController : ControllerBase
    {
        private readonly IVariationService _variationService;
        public VariationController(IVariationService _variationService)
        {
            this._variationService = _variationService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("allvariations")]
        public async Task<IActionResult> GetAllVariationsAsync()
        {
            try
            {
                var response = await _variationService.GetAllVariationsAsync();
                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<IEnumerable<Variation>>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message
                    });
            }
        }

        [AllowAnonymous]
        [HttpGet("allvariationsbycategoryId/{categoryId}")]
        public async Task<IActionResult> GetAllVariationsByCategoryIdAsync([FromRoute] Guid categoryId)
        {
            try
            {
                var response = await _variationService.GetAllVariationsByCategoryIdAsync(categoryId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<IEnumerable<Variation>>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message
                    });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("addvariation")]
        public async Task<IActionResult> AddVariationAsync([FromBody] VariationDto variationDto)
        {
            try
            {
                var response = await _variationService.AddVariationAsync(variationDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<Variation>
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = ex.Message,
                    ResponseObject = new Variation()
                });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("updatevariation")]
        public async Task<IActionResult> UpdateVariationAsync([FromBody] VariationDto variationDto)
        {
            try
            {
                var response = await _variationService.UpdateVariationAsync(variationDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<Variation>
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = ex.Message,
                    ResponseObject = new Variation()
                });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("deletevariation/{variationId}")]
        public async Task<IActionResult> DeleteVariationByIdAsync([FromRoute] Guid variationId)
        {
            try
            {
                var response = await _variationService.DeleteVariationByIdAsync(variationId);
                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<Variation>
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = ex.Message,
                    ResponseObject = new Variation()
                });
            }
        }

        [AllowAnonymous]
        [HttpGet("getvariation/{variationId}")]
        public async Task<IActionResult> GetVariationByIdAsync([FromRoute] Guid variationId)
        {
            try
            {
                var response = await _variationService.GetVariationByIdAsync(variationId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<Variation>
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = ex.Message,
                    ResponseObject = new Variation()
                });
            }
        }

    }
}
