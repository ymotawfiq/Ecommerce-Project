using Ecommerce.Data.DTOs;
using Ecommerce.Data.Extensions;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Repository.Repositories.ProductCategoryRepository;
using Ecommerce.Repository.Repositories.VariationRepository;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VariationController : ControllerBase
    {
        private readonly IVariation _variationRepository;
        private readonly IProductCategory _categoryRepository;
        public VariationController(IVariation _variationRepository, IProductCategory _categoryRepository)
        {
            this._categoryRepository = _categoryRepository;
            this._variationRepository = _variationRepository;
        }

        [HttpGet("allvariations")]
        public async Task<IActionResult> GetAllVariationsAsync()
        {
            try
            {
                var variations = await _variationRepository.GetAllVariationsAsync();
                if (variations.ToList().Count == 0)
                {
                    return StatusCode(StatusCodes.Status200OK
                    , new ApiResponse<IEnumerable<Variation>>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = "No variations founded",
                        ResponseObject = variations
                    });
                }
                return StatusCode(StatusCodes.Status200OK
                    , new ApiResponse<IEnumerable<Variation>>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = "Variations founded successfully",
                        ResponseObject = variations
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
        public async Task<IActionResult> GetAllVariationsAsync([FromRoute] Guid categoryId)
        {
            try
            {
                var variations = await _variationRepository.GetAllVariationsByCategoryIdAsync(categoryId);
                if (variations.ToList().Count == 0)
                {
                    return StatusCode(StatusCodes.Status200OK
                    , new ApiResponse<IEnumerable<Variation>>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = "No variations founded",
                        ResponseObject = variations
                    });
                }
                return StatusCode(StatusCodes.Status200OK
                    , new ApiResponse<IEnumerable<Variation>>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = "Variations founded successfully",
                        ResponseObject = variations
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
                if (variationDto == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<Variation>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Variation must not be null",
                        ResponseObject = new Variation()
                    });
                }
                ProductCategory productCategory = await _categoryRepository.GetCategoryByIdAsync
                    (new Guid(variationDto.CatrgoryId));
                if (productCategory == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<Variation>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = $"Category with id ({variationDto.CatrgoryId}) not exists",
                        ResponseObject = new Variation()
                    });
                }
                Variation variation = await _variationRepository.AddVariationAsync(
                    ConvertFromDto.ConvertFromVariationDto_Add(variationDto));
                return StatusCode(StatusCodes.Status201Created, new ApiResponse<Variation>
                {
                    StatusCode = 201,
                    IsSuccess = true,
                    Message = "Variation created successfully",
                    ResponseObject = variation
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
                if (variationDto == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<Variation>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Variation must not be null",
                        ResponseObject = new Variation()
                    });
                }
                if (variationDto.Id == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<Variation>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Variation id must not be null",
                        ResponseObject = new Variation()
                    });
                }
                ProductCategory productCategory = await _categoryRepository.GetCategoryByIdAsync
                    (new Guid(variationDto.CatrgoryId));
                if (productCategory == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<Variation>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = $"Category with id ({variationDto.CatrgoryId}) not exists",
                        ResponseObject = new Variation()
                    });
                }
                Variation oldVariation = await _variationRepository.GetVariationByIdAsync
                    (new Guid(variationDto.Id));
                if(oldVariation == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<Variation>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = $"No variations founded with id ({variationDto.Id})",
                        ResponseObject = new Variation()
                    });   
                }
                Variation variation = await _variationRepository.UpdateVariationAsync(
                    ConvertFromDto.ConvertFromVariationDto_Update(variationDto));
                return StatusCode(StatusCodes.Status200OK, new ApiResponse<Variation>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Variation updated successfully",
                    ResponseObject = variation
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
                Variation variation = await _variationRepository.GetVariationByIdAsync(variationId);
                if(variation == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new ApiResponse<Variation>
                    {
                        StatusCode = 404,
                        IsSuccess = false,
                        Message = $"No variations founded with id ({variationId})",
                        ResponseObject = new Variation()
                    });
                }
                var deletedVariation = await _variationRepository.DeleteVariationByIdAsync(variationId);
                return StatusCode(StatusCodes.Status200OK, new ApiResponse<Variation>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Variation deleted successfully",
                    ResponseObject = deletedVariation
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
                Variation variation = await _variationRepository.GetVariationByIdAsync(variationId);
                if (variation == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new ApiResponse<Variation>
                    {
                        StatusCode = 404,
                        IsSuccess = false,
                        Message = $"No variations founded with id ({variationId})",
                        ResponseObject = new Variation()
                    });
                }
                return StatusCode(StatusCodes.Status200OK, new ApiResponse<Variation>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Variation founded successfully",
                    ResponseObject = variation
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
