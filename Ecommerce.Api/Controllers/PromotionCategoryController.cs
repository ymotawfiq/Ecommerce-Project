using Ecommerce.Data.DTOs;
using Ecommerce.Data.Extensions;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Repository.Repositories.ProductCategoryRepository;
using Ecommerce.Repository.Repositories.PromotionCategoryRepository;
using Ecommerce.Repository.Repositories.PromotionRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionCategoryController : ControllerBase
    {
        private readonly IPromotionCategory _promotionCategoryRepository;
        private readonly IProductCategory _productCategoryRepository;
        private readonly IPromotion _promotionRepository;
        public PromotionCategoryController
            (
            IPromotionCategory _promotionCategoryRepository,
            IProductCategory _productCategoryRepository,
            IPromotion _promotionRepository
            )
        {
            this._productCategoryRepository = _productCategoryRepository;
            this._promotionCategoryRepository = _promotionCategoryRepository;
            this._promotionRepository = _promotionRepository;
        }


        [HttpGet("allpromotioncategory")]
        public IActionResult GetAllPromotionCategories()
        {
            try
            {
                var promotionCategories = _promotionCategoryRepository.GetAllPromotions();
                if (promotionCategories.ToList().Count == 0)
                {
                    return StatusCode(StatusCodes.Status200OK
                    , new ApiResponse<IEnumerable<PromotionCategory>>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = "No promotion category founded",
                        ResponseObject = promotionCategories
                    });
                }
                return StatusCode(StatusCodes.Status200OK
                    , new ApiResponse<IEnumerable<PromotionCategory>>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = "Promotion category founded successfully",
                        ResponseObject = promotionCategories
                    });
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<IEnumerable<PromotionCategory>>
                    {
                        StatusCode = 500,
                        IsSuccess = true,
                        Message = ex.Message,
                        ResponseObject = new List<PromotionCategory>()
                    });
            }
        }


        [HttpGet("allpromotioncategorybycategoryid/{categoryId}")]
        public IActionResult GetAllPromotionCategoriesByCategoryId([FromRoute] Guid categoryId)
        {
            try
            {
                var promotionCategories = _promotionCategoryRepository.GetAllPromotionsByCategoryId(categoryId);
                if (promotionCategories.ToList().Count == 0)
                {
                    return StatusCode(StatusCodes.Status200OK
                    , new ApiResponse<IEnumerable<PromotionCategory>>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = $"No promotion category founded with category id ({categoryId})",
                        ResponseObject = promotionCategories
                    });
                }
                return StatusCode(StatusCodes.Status200OK
                    , new ApiResponse<IEnumerable<PromotionCategory>>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = "Promotion category founded successfully",
                        ResponseObject = promotionCategories
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<IEnumerable<PromotionCategory>>
                    {
                        StatusCode = 500,
                        IsSuccess = true,
                        Message = ex.Message,
                        ResponseObject = new List<PromotionCategory>()
                    });
            }
        }

        [HttpGet("allpromotioncategorybypromotionid/{promotionId}")]
        public IActionResult GetAllPromotionCategoriesByPromotionId([FromRoute] Guid promotionId)
        {
            try
            {
                var promotionCategories = _promotionCategoryRepository.GetAllPromotionsByPromotionId(promotionId);
                if (promotionCategories.ToList().Count == 0)
                {
                    return StatusCode(StatusCodes.Status200OK
                    , new ApiResponse<IEnumerable<PromotionCategory>>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = $"No promotion category founded with promotion id ({promotionId})",
                        ResponseObject = promotionCategories
                    });
                }
                return StatusCode(StatusCodes.Status200OK
                    , new ApiResponse<IEnumerable<PromotionCategory>>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = "Promotion category founded successfully",
                        ResponseObject = promotionCategories
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<IEnumerable<PromotionCategory>>
                    {
                        StatusCode = 500,
                        IsSuccess = true,
                        Message = ex.Message,
                        ResponseObject = new List<PromotionCategory>()
                    });
            }
        }

        [HttpPost("addpromotioncategory")]
        public IActionResult AddPromotionCategory([FromBody] PromotionCategoryDto promotionCategoryDto)
        {
            try
            {
                if (promotionCategoryDto == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<PromotionCategory>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Input must not be null",
                        ResponseObject = new PromotionCategory()
                    });
                }
                PromotionCategory promotionCategory = _promotionCategoryRepository.AddPromotionCategory(
                    ConvertFromDto.ConvertFromPromotionCategoryDto_Add(promotionCategoryDto));
                return StatusCode(StatusCodes.Status201Created
                    , new ApiResponse<PromotionCategory>
                    {
                        StatusCode = 201,
                        IsSuccess = true,
                        Message = "Promotion category created successfully",
                        ResponseObject = promotionCategory
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<PromotionCategory>
                    {
                        StatusCode = 500,
                        IsSuccess = true,
                        Message = ex.Message,
                        ResponseObject = new PromotionCategory()
                    });
            }
        }

        [HttpPut("updatepromotioncategory")]
        public IActionResult UpdatePromotionCategory([FromBody] PromotionCategoryDto promotionCategoryDto)
        {
            try
            {
                if (promotionCategoryDto == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<PromotionCategory>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Input must not be null",
                        ResponseObject = new PromotionCategory()
                    });
                }
                if (promotionCategoryDto.Id == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<PromotionCategory>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Id must not be null",
                        ResponseObject = new PromotionCategory()
                    });
                }
                Promotion promotion = _promotionRepository.GetPromotionById(promotionCategoryDto.PromotionId);
                if (promotion == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<PromotionCategory>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = $"Promotion with id ({promotionCategoryDto.PromotionId}) not exists",
                        ResponseObject = new PromotionCategory()
                    });
                }
                ProductCategory productCategory = _productCategoryRepository.GetCategoryById
                    (promotionCategoryDto.CategoryId);
                if (productCategory == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<PromotionCategory>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = $"Category with id ({promotionCategoryDto.CategoryId}) not exists",
                        ResponseObject = new PromotionCategory()
                    });
                }
                PromotionCategory promotionCategory = _promotionCategoryRepository.UpdatePromotionCategory(
                    ConvertFromDto.ConvertFromPromotionCategoryDto_Update(promotionCategoryDto));
                return StatusCode(StatusCodes.Status200OK
                    , new ApiResponse<PromotionCategory>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = "Promotion category updated successfully",
                        ResponseObject = promotionCategory
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<PromotionCategory>
                    {
                        StatusCode = 500,
                        IsSuccess = true,
                        Message = ex.Message,
                        ResponseObject = new PromotionCategory()
                    });
            }
        }


        [HttpGet("getpromotioncategorybyid/{promotionCategoryId}")]
        public IActionResult GetPromotionCategoryById([FromRoute] Guid promotionCategoryId)
        {
            try
            {
                PromotionCategory promotionCategory = _promotionCategoryRepository
                    .GetPromotionCategoryById(promotionCategoryId);
                if (promotionCategory == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<PromotionCategory>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = $"Promotion category with id ({promotionCategoryId}) not exists",
                        ResponseObject = new PromotionCategory()
                    });
                }
                return StatusCode(StatusCodes.Status200OK
                    , new ApiResponse<PromotionCategory>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = $"Promotion category founded successfully",
                        ResponseObject = promotionCategory
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<PromotionCategory>
                    {
                        StatusCode = 500,
                        IsSuccess = true,
                        Message = ex.Message,
                        ResponseObject = new PromotionCategory()
                    });
            }
        }


        [HttpDelete("deletepromotioncategorybyid/{promotionCategoryId}")]
        public IActionResult DeletePromotionCategoryById([FromRoute] Guid promotionCategoryId)
        {
            try
            {
                PromotionCategory promotionCategory = _promotionCategoryRepository
                    .GetPromotionCategoryById(promotionCategoryId);
                if (promotionCategory == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<PromotionCategory>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = $"Promotion category with id ({promotionCategoryId}) not exists",
                        ResponseObject = new PromotionCategory()
                    });
                }
                PromotionCategory deletedPromotionCategory = _promotionCategoryRepository
                    .DeletePromotionCategoryById(promotionCategoryId);
                return StatusCode(StatusCodes.Status200OK
                    , new ApiResponse<PromotionCategory>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = $"Promotion category deleted successfully",
                        ResponseObject = deletedPromotionCategory
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<PromotionCategory>
                    {
                        StatusCode = 500,
                        IsSuccess = true,
                        Message = ex.Message,
                        ResponseObject = new PromotionCategory()
                    });
            }
        }


    }
}
