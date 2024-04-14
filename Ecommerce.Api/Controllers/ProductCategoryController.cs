using Ecommerce.Data.DTOs;
using Ecommerce.Data.Extensions;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Repository.Repositories.ProductCategoryRepository;
using Ecommerce.Repository.Repositories.ProductImagesRepository;
using Ecommerce.Repository.Repositories.ProductRepository;
using Ecommerce.Service.Services.ProductCategoryService;
using Microsoft.AspNetCore.Authorization;
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

        [AllowAnonymous]
        [HttpGet("allcategories")]
        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            try
            {
                var response = await _cateegoryService.GetAllCategoriesAsync();
                return Ok(response);
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

        [Authorize(Roles = "Admin")]
        [HttpPost("addcategory")]
        public async Task<IActionResult> AddCategoryAsync([FromBody] ProductCategoryDto productCategoryDto)
        {
            try
            {
                var response = await _cateegoryService.AddCategoryAsync(productCategoryDto);
                return Ok(response);
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

        [AllowAnonymous]
        [HttpGet("getcategorybyid/{categoryId}")]
        public async Task<IActionResult> GetCategoryByIdAsync([FromRoute] Guid categoryId)
        {
            try
            {
                var response = await _cateegoryService.GetCategoryByIdAsync(categoryId);
                return Ok(response);
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

        [Authorize(Roles = "Admin")]
        [HttpPut("updatecategory")]
        public async Task<IActionResult> UpdateCategoryAsync(ProductCategoryDto productCategoryDto)
        {
            try
            {
                var response = await _cateegoryService.UpdateCategoryAsync(productCategoryDto);
                return Ok(response);
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

        [Authorize(Roles = "Admin")]
        [HttpDelete("deletecategory/{categoryId}")]
        public async Task<IActionResult> DeleteCategoryByCategoryIdAsync([FromRoute] Guid categoryId)
        {
            try
            {
                var response = await _cateegoryService.DeleteCategoryByCategoryIdAsync(categoryId);
                return Ok(response);
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
