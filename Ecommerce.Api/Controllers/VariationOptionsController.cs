using Ecommerce.Data.DTOs;
using Ecommerce.Data.Extensions;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Repository.Repositories.VariationOptionsRepository;
using Ecommerce.Repository.Repositories.VariationRepository;
using Ecommerce.Service.Services.VariationOptionService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [ApiController]
    public class VariationOptionsController : ControllerBase
    {
        private readonly IVariationOptionsService _variationOptionsService;
        public VariationOptionsController(IVariationOptionsService _variationOptionsService)
        {
            this._variationOptionsService = _variationOptionsService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("variationOptions")]
        public async Task<IActionResult> GetAllVariationOptionsAsync()
        {
            try
            {
                var response = await _variationOptionsService.GetAllVariationOptionsAsync();
                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<IEnumerable<VariationOptions>>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message,
                        ResponseObject = new List<VariationOptions>()
                    });
            }
        }

        [AllowAnonymous]
        [HttpGet("variationOprionsByVariation/{variationId}")]
        public async Task<IActionResult> GetAllVariationOptionsByVariationIdAsync([FromRoute] Guid variationId)
        {
            try
            {
                var response = await _variationOptionsService.GetAllVariationOptionsByVariationIdAsync(variationId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<IEnumerable<VariationOptions>>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message,
                        ResponseObject = new List<VariationOptions>()
                    });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("addVariationOption")]
        public async Task<IActionResult> AddVariationOptionAsync(VariationOptionsDto variationOptionsDto)
        {
            try
            {
                var response = await _variationOptionsService.AddVariationOptionAsync(variationOptionsDto);
                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<VariationOptions>
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = ex.Message,
                    ResponseObject = new VariationOptions()
                });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("updateVariationOption")]
        public async Task<IActionResult> UpdateVariationOptionAsync(VariationOptionsDto variationOptionsDto)
        {
            try
            {
                var response = await _variationOptionsService.UpdateVariationOptionAsync(variationOptionsDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<VariationOptions>
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = ex.Message,
                    ResponseObject = new VariationOptions()
                });
            }
        }

        [AllowAnonymous]
        [HttpGet("variationOption/{variationOptionId}")]
        public async Task<IActionResult> GetVariationOptionByVariationIdAsync([FromRoute] Guid variationOptionId)
        {
            try
            {
                var response = await _variationOptionsService.GetVariationOptionByVariationIdAsync(variationOptionId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<VariationOptions>
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = ex.Message,
                    ResponseObject = new VariationOptions()
                });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("deleteVariationOption/{variationOptionId}")]
        public async Task<IActionResult> DeleteVariationOptionByVariationIdAsync([FromRoute] Guid variationOptionId)
        {
            try
            {
                var response = await _variationOptionsService.DeleteVariationOptionByVariationIdAsync(variationOptionId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<VariationOptions>
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = ex.Message,
                    ResponseObject = new VariationOptions()
                });
            }
        }


    }
}
