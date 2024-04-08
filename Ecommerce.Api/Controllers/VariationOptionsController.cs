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
        public IActionResult GetAllVariationOptions()
        {
            try
            {
                var variationOptions = _variationOptionsRepository.GetAllVariationOptions();
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
        public IActionResult GetAllVariationOptionsByVariationId([FromRoute] Guid variationId)
        {
            try
            {
                var variationOptions = _variationOptionsRepository.GetAllVariationOptionsByVariationId(variationId);
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
        public IActionResult AddVariationOption(VariationOptionsDto variationOptionsDto)
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
                Variation variation = _variationRepository.GetVariationById(new Guid(variationOptionsDto.VariationId));
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
                VariationOptions variationOptions = _variationOptionsRepository.AddVariationOptions(
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
        public IActionResult UpdateVariationOption(VariationOptionsDto variationOptionsDto)
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
                Variation variation = _variationRepository.GetVariationById(new Guid(variationOptionsDto.VariationId));
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
                VariationOptions variationOptions = _variationOptionsRepository.UpdateVariationOptions(
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
        public IActionResult GetVariationOptionByVariationId([FromRoute] Guid variationOptionId)
        {
            try
            {
                VariationOptions variationOptions = _variationOptionsRepository.GetVariationOptionsById(variationOptionId);
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
        public IActionResult DeleteVariationOptionByVariationId([FromRoute] Guid variationOptionId)
        {
            try
            {
                VariationOptions variationOptions = _variationOptionsRepository.GetVariationOptionsById(variationOptionId);
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
                VariationOptions deletedVariationOption = _variationOptionsRepository.DeleteVariationOptionsById(variationOptionId);
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
