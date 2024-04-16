using Ecommerce.Data.DTOs;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;

using Ecommerce.Service.Services.ProductService.ProductService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService _productService)
        {
            this._productService = _productService;
        }

        [AllowAnonymous]
        [HttpGet("all-products")]
        public async Task<IActionResult> GetAllProductsAsync()
        {
            try
            {
                var response = await _productService.GetAllProductsAsync();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,new ApiResponse<IEnumerable<Product>>
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = ex.Message,
                    ResponseObject = new List<Product>()
                });
            }

        }

        [AllowAnonymous]
        [HttpGet("allProductsByCategory/{categoryId}")]
        public async Task<IActionResult> GetAllProductsByCategoryIdAsync([FromRoute] Guid categoryId)
        {
            try
            {
                var response = await _productService.GetAllProductsByCategoryIdAsync(categoryId);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<IEnumerable<Product>>
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = ex.Message,
                    ResponseObject = new List<Product>()
                });
            }

        }

        [Authorize(Roles = "Admin")]
        [HttpPost("add-product")]
        public async Task<IActionResult> AddProductAsync([FromForm] ProductDto productDto)
        {
            try
            {
                var response = await _productService.AddProductAsync(productDto);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<Product>
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = ex.Message,
                    ResponseObject = new Product()
                });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-product/{productId}")]
        public async Task<IActionResult> DeleteProductAsync([FromRoute] Guid productId)
        {
            try
            {
                var response = await _productService.DeleteProductAsync(productId);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<Product>
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = ex.Message,
                    ResponseObject = new Product()
                });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update-product")]
        public async Task<IActionResult> UpdateProductAsync([FromForm] ProductDto productDto)
        {
            try
            {
                var response = await _productService.UpdateProductAsync(productDto);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<Product>
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = ex.Message,
                    ResponseObject = new Product()
                });
            }
        }

        [AllowAnonymous]
        [HttpGet("product/{productId}")]
        public async Task<IActionResult> GetProductByProductIdAsync([FromRoute] Guid productId)
        {
            try
            {
                var response = await _productService.GetProductByProductIdAsync(productId);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<Product>
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = ex.Message,
                    ResponseObject = new Product()
                });
            }
        }

        
    }
}
