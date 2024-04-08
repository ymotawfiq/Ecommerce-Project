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
        public IActionResult GetAllVariations()
        {
            try
            {
                var variations = _variationRepository.GetAllVariations();
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
        public IActionResult GetAllVariations([FromRoute] Guid categoryId)
        {
            try
            {
                var variations = _variationRepository.GetAllVariationsByCategoryId(categoryId);
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
        public IActionResult AddVariation([FromBody] VariationDto variationDto)
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
                ProductCategory productCategory = _categoryRepository.GetCategoryById(new Guid(variationDto.CatrgoryId));
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
                Variation variation = _variationRepository.AddVariation(
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
        public IActionResult UpdateVariation([FromBody] VariationDto variationDto)
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
                ProductCategory productCategory = _categoryRepository.GetCategoryById(new Guid(variationDto.CatrgoryId));
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
                Variation oldVariation = _variationRepository.GetVariationById(new Guid(variationDto.Id));
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
                Variation variation = _variationRepository.UpdateVariation(
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
        public IActionResult DeleteVariationById([FromRoute] Guid variationId)
        {
            try
            {
                Variation variation = _variationRepository.GetVariationById(variationId);
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
                _variationRepository.DeleteVariationById(variationId);
                return StatusCode(StatusCodes.Status200OK, new ApiResponse<Variation>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Variation deleted successfully",
                    ResponseObject = variation
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
        public IActionResult GetVariationById([FromRoute] Guid variationId)
        {
            try
            {
                Variation variation = _variationRepository.GetVariationById(variationId);
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
