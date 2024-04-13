using Ecommerce.Data.DTOs;
using Ecommerce.Data.Extensions;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Repository.Repositories.VariationOptionsRepository;
using Ecommerce.Repository.Repositories.VariationRepository;
using Ecommerce.Service.Services.VariationOptionService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VariationOptionsController : ControllerBase
    {
        private readonly IVariationOptionsService _variationOptionsService;
        public VariationOptionsController(IVariationOptionsService _variationOptionsService)
        {
            this._variationOptionsService = _variationOptionsService;
        }

        [HttpGet("allvariationoprions")]
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

        [HttpGet("allvariationoprionsbyvariationid/{variationId}")]
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

        [HttpPost("addvariationoption")]
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

        [HttpPut("updatevariationoption")]
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

        [HttpGet("getvariationoption/{variationOptionId}")]
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

        [HttpDelete("deletevariationoption/{variationOptionId}")]
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
