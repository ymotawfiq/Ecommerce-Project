using Ecommerce.Data.DTOs;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;

using Ecommerce.Service.Services.VariationService;
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

        [HttpGet("allvariations")]
        public async Task<IActionResult> GetAllVariationsAsync()
        {
            try
            {
                var response = await _variationService.GetAllVariationsAsync();
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<IEnumerable<Variation>>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Unknown error",
                        ResponseObject = new List<Variation>()
                    });
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

        [HttpGet("allvariationsbycategoryId/{categoryId}")]
        public async Task<IActionResult> GetAllVariationsByCategoryIdAsync([FromRoute] Guid categoryId)
        {
            try
            {
                var response = await _variationService.GetAllVariationsByCategoryIdAsync(categoryId);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<IEnumerable<Variation>>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Unknown error",
                        ResponseObject = new List<Variation>()
                    });
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

        [HttpPost("addvariation")]
        public async Task<IActionResult> AddVariationAsync([FromBody] VariationDto variationDto)
        {
            try
            {
                var response = await _variationService.AddVariationAsync(variationDto);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<Variation>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Unknown error",
                        ResponseObject = new Variation()
                    });
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

        [HttpPut("updatevariation")]
        public async Task<IActionResult> UpdateVariationAsync([FromBody] VariationDto variationDto)
        {
            try
            {
                var response = await _variationService.UpdateVariationAsync(variationDto);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<Variation>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Unknown error",
                        ResponseObject = new Variation()
                    });
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

        [HttpDelete("deletevariation/{variationId}")]
        public async Task<IActionResult> DeleteVariationByIdAsync([FromRoute] Guid variationId)
        {
            try
            {
                var response = await _variationService.DeleteVariationByIdAsync(variationId);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<Variation>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Unknown error",
                        ResponseObject = new Variation()
                    });
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

        [HttpGet("getvariation/{variationId}")]
        public async Task<IActionResult> GetVariationByIdAsync([FromRoute] Guid variationId)
        {
            try
            {
                var response = await _variationService.GetVariationByIdAsync(variationId);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<Variation>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Unknown error",
                        ResponseObject = new Variation()
                    });
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
