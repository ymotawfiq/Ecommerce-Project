using Ecommerce.Data.DTOs;
using Ecommerce.Data.Extensions;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Repository.Repositories.ProductCategoryRepository;
using Ecommerce.Repository.Repositories.ProductImagesRepository;
using Ecommerce.Repository.Repositories.ProductRepository;
using Ecommerce.Service.Services.ProductCategoryService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryService _cateegoryService;
        public ProductCategoryController(IProductCategoryService _cateegoryService)
        {
            this._cateegoryService = _cateegoryService;
        }

        [HttpGet("allcategories")]
        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            try
            {
                var response = await _cateegoryService.GetAllCategoriesAsync();
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<IEnumerable<ProductCategory>>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Unknown error",
                    ResponseObject = new List<ProductCategory>()
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<IEnumerable<ProductCategory>>
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Message = ex.Message,
                    ResponseObject = new List<ProductCategory>()
                });
            }
        }

        [HttpPost("addcategory")]
        public async Task<IActionResult> AddCategoryAsync([FromBody] ProductCategoryDto productCategoryDto)
        {
            try
            {
                var response = await _cateegoryService.AddCategoryAsync(productCategoryDto);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<ProductCategory>
                    {
                        IsSuccess = false,
                        StatusCode = 400,
                        Message = "Unknown error",
                        ResponseObject = new ProductCategory()
                    });
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<ProductCategory>
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Message = ex.Message,
                    ResponseObject = new ProductCategory()
                });
            }
        }

        [HttpGet("getcategorybyid/{categoryId}")]
        public async Task<IActionResult> GetCategoryByIdAsync([FromRoute] Guid categoryId)
        {
            try
            {
                var response = await _cateegoryService.GetCategoryByIdAsync(categoryId);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<ProductCategory>
                    {
                        IsSuccess = false,
                        StatusCode = 400,
                        Message = "Unknown error",
                        ResponseObject = new ProductCategory()
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<ProductCategory>
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Message = ex.Message,
                    ResponseObject = new ProductCategory()
                });
            }
        }

        [HttpPut("updatecategory")]
        public async Task<IActionResult> UpdateCategoryAsync(ProductCategoryDto productCategoryDto)
        {
            try
            {
                var response = await _cateegoryService.UpdateCategoryAsync(productCategoryDto);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<ProductCategory>
                    {
                        IsSuccess = false,
                        StatusCode = 400,
                        Message = "Unknown error",
                        ResponseObject = new ProductCategory()
                    });
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<ProductCategory>
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Message = ex.Message,
                    ResponseObject = new ProductCategory()
                });
            }
        }

        [HttpDelete("deletecategory/{categoryId}")]
        public async Task<IActionResult> DeleteCategoryByCategoryIdAsync([FromRoute] Guid categoryId)
        {
            try
            {
                var response = await _cateegoryService.DeleteCategoryByCategoryIdAsync(categoryId);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<ProductCategory>
                    {
                        IsSuccess = false,
                        StatusCode = 400,
                        Message = "Unknown error",
                        ResponseObject = new ProductCategory()
                    });
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<ProductCategory>
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Message = ex.Message,
                    ResponseObject = new ProductCategory()
                });
            }
        }

    }
}
