using Ecommerce.Data.DTOs;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;

using Ecommerce.Service.Services.ProductService.ProductService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService _productService)
        {
            this._productService = _productService;
        }

        [AllowAnonymous]
        [HttpGet("allproducts")]
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
        [HttpGet("allproductsbycategoryid/{categoryId}")]
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
        [HttpPost("addproduct")]
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
        [HttpDelete("deleteproduct/{productId}")]
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
        [HttpPut("updateproduct")]
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
        [HttpGet("getproduct/{productId}")]
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
