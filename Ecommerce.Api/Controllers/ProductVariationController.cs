using Ecommerce.Data.DTOs;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Service.Services.ProductVariationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [ApiController]
    public class ProductVariationController : ControllerBase
    {
        private readonly IProductVariationService _productVariationService;
        public ProductVariationController(IProductVariationService _productVariationService)
        {
            this._productVariationService = _productVariationService;
        }

        [AllowAnonymous]
        [HttpGet("allProductVariations")]
        public async Task<IActionResult> GetAllProductVariationsAsync()
        {
            try
            {
                var response = await _productVariationService.GetAllProductVariationsAsync();
                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<IEnumerable<ProductVariation>>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message,
                        ResponseObject = new List<ProductVariation>()
                    });
            }
        }


        [AllowAnonymous]
        [HttpGet("productVariationsByVariationOption/{variationOptionId}")]
        public async Task<IActionResult> GetAllProductVariationsByVariationOptionIdAsync
            ([FromRoute]Guid variationOptionId)
        {
            try
            {
                var response = await _productVariationService
                    .GetAllProductVariationsByVariationOptionIdAsync(variationOptionId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<IEnumerable<ProductVariation>>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message,
                        ResponseObject = new List<ProductVariation>()
                    });
            }
        }


        [AllowAnonymous]
        [HttpGet("productVariationsByProductItem/{productItemId}")]
        public async Task<IActionResult> GetAllProductVariationsByProductItemIdAsync
            ([FromRoute] Guid productItemId)
        {
            try
            {
                var response = await _productVariationService
                    .GetAllProductVariationsByProductItemIdAsync(productItemId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<IEnumerable<ProductVariation>>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message,
                        ResponseObject = new List<ProductVariation>()
                    });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("addProductVariation")]
        public async Task<IActionResult> AddProductVariationAsync
            ([FromBody] ProductVariationDto productVariationDto)
        {
            try
            {
                var response = await _productVariationService.AddProductVariationAsync(productVariationDto);
                return Ok(response);

            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<ProductVariation>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message,
                        ResponseObject = new ProductVariation()
                    });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("updateProductVariation")]
        public async Task<IActionResult> UpdateProductVariationAsync
            ([FromBody] ProductVariationDto productVariationDto)
        {
            try
            {
                var response = await _productVariationService.UpdateProductVariationAsync(productVariationDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<ProductVariation>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message,
                        ResponseObject = new ProductVariation()
                    });
            }
        }

        [AllowAnonymous]
        [HttpGet("productVariation/{productVariationId}")]
        public async Task<IActionResult> GetProductVariationByIdAsync([FromRoute] Guid productVariationId)
        {
            try
            {
                var response = await _productVariationService.GetProductVariationByIdAsync(productVariationId);
                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<ProductVariation>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message,
                        ResponseObject = new ProductVariation()
                    });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("deleteProductVariation/{productVariationId}")]
        public async Task<IActionResult> DeleteProductVariationByIdAsync([FromRoute] Guid productVariationId)
        {
            try
            {
                var response = await _productVariationService
                    .DeleteProductVariationByIdAsync(productVariationId);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return StatusCode(StatusCodes.Status400BadRequest
                    , new ApiResponse<ProductVariation>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Unknown error",
                        ResponseObject = new ProductVariation()
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new ApiResponse<ProductVariation>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message = ex.Message,
                        ResponseObject = new ProductVariation()
                    });
            }
        }


    }
}
