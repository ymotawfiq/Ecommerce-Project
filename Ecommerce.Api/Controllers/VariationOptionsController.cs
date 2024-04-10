using Ecommerce.Data.DTOs;
using Ecommerce.Data.Extensions;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Repository.Repositories.VariationOptionsRepository;
using Ecommerce.Repository.Repositories.VariationRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VariationOptionsController : ControllerBase
    {
        private readonly IVariationOptions _variationOptionsRepository;
        private readonly IVariation _variationRepository;
        public VariationOptionsController(IVariationOptions _variationOptionsRepository,
            IVariation _variationRepository)
        {
            this._variationOptionsRepository = _variationOptionsRepository;
            this._variationRepository = _variationRepository;
        }

        [HttpGet("allvariationoprions")]
        public async Task<IActionResult> GetAllVariationOptionsAsync()
        {
            try
            {
                var variationOptions = await _variationOptionsRepository.GetAllVariationOptionsAsync();
                if (variationOptions.ToList().Count == 0)
                {
                    return StatusCode(StatusCodes.Status200OK
                    , new ApiResponse<IEnumerable<VariationOptions>>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = "No variation options founded",
                        ResponseObject = variationOptions
                    });
                }
                return StatusCode(StatusCodes.Status200OK
                    , new ApiResponse<IEnumerable<VariationOptions>>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = "Variation options founded successfully",
                        ResponseObject = variationOptions
                    });
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
                var variationOptions = await _variationOptionsRepository.GetAllVariationOptionsByVariationIdAsync
                    (variationId);
                if (variationOptions.ToList().Count == 0)
                {
                    return StatusCode(StatusCodes.Status200OK
                    , new ApiResponse<IEnumerable<VariationOptions>>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = "No variation options founded",
                        ResponseObject = variationOptions
                    });
                }
                return StatusCode(StatusCodes.Status200OK
                    , new ApiResponse<IEnumerable<VariationOptions>>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = "Variation options founded successfully",
                        ResponseObject = variationOptions
                    });
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
                if(variationOptionsDto == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<VariationOptions>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Input must not be null",
                        ResponseObject = new VariationOptions()
                    });
                }
                Variation variation = await _variationRepository.GetVariationByIdAsync
                    (new Guid(variationOptionsDto.VariationId));
                if (variation == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<VariationOptions>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = $"No variations founded with id ({variationOptionsDto.VariationId})",
                        ResponseObject = new VariationOptions()
                    });
                }
                VariationOptions variationOptions = await _variationOptionsRepository.AddVariationOptionsAsync(
                    ConvertFromDto.ConvertFromVariationOptions_Add(variationOptionsDto));
                return StatusCode(StatusCodes.Status201Created, new ApiResponse<VariationOptions>
                {
                    StatusCode = 201,
                    IsSuccess = true,
                    Message = $"Variation option created successfully",
                    ResponseObject = variationOptions
                });
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
                if (variationOptionsDto == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<VariationOptions>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Input must not be null",
                        ResponseObject = new VariationOptions()
                    });
                }
                if(variationOptionsDto.Id == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<VariationOptions>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Variation option id must not be null",
                        ResponseObject = new VariationOptions()
                    });
                }
                Variation variation = await _variationRepository.GetVariationByIdAsync
                    (new Guid(variationOptionsDto.VariationId));
                if (variation == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<VariationOptions>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = $"No variations founded with id ({variationOptionsDto.VariationId})",
                        ResponseObject = new VariationOptions()
                    });
                }
                VariationOptions variationOptions = await _variationOptionsRepository.UpdateVariationOptionsAsync(
                    ConvertFromDto.ConvertFromVariationOptions_Update(variationOptionsDto));
                return StatusCode(StatusCodes.Status200OK, new ApiResponse<VariationOptions>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = $"Variation option updated successfully",
                    ResponseObject = variationOptions
                });
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
                VariationOptions variationOptions = await _variationOptionsRepository.GetVariationOptionsByIdAsync
                    (variationOptionId);
                if (variationOptions == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<VariationOptions>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = $"No variation options founded with id ({variationOptionId})",
                        ResponseObject = new VariationOptions()
                    });
                }
                return StatusCode(StatusCodes.Status200OK, new ApiResponse<VariationOptions>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Variation option founded successfully",
                    ResponseObject = variationOptions
                });
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
                VariationOptions variationOptions = await _variationOptionsRepository
                    .GetVariationOptionsByIdAsync(variationOptionId);
                if (variationOptions == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<VariationOptions>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = $"No variation options founded with id ({variationOptionId})",
                        ResponseObject = new VariationOptions()
                    });
                }
                VariationOptions deletedVariationOption = await _variationOptionsRepository
                    .DeleteVariationOptionsByIdAsync(variationOptionId);
                return StatusCode(StatusCodes.Status200OK, new ApiResponse<VariationOptions>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Variation option deleted successfully",
                    ResponseObject = deletedVariationOption
                });
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
