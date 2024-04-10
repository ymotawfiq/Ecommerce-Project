using Ecommerce.Data.DTOs;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;

using Ecommerce.Service.Services.ProductService.ProductService;

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

        [HttpGet("allproducts")]
        public async Task<IActionResult> GetAllProductsAsync()
        {
            try
            {
                var response = await _productService.GetAllProductsAsync();

                if (response.IsSuccess)
                {
                    return Ok(response);
                }

                return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<IEnumerable<Product>>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Unknown error",
                    ResponseObject = new List<Product>()
                });
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

        [HttpGet("allproductsbycategoryid/{categoryId}")]
        public async Task<IActionResult> GetAllProductsByCategoryIdAsync([FromRoute] Guid categoryId)
        {
            try
            {
                var response = await _productService.GetAllProductsByCategoryIdAsync(categoryId);

                if (response.IsSuccess)
                {
                    return Ok(response);
                }

                return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<IEnumerable<Product>>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Unknown error",
                    ResponseObject = new List<Product>()
                }); ;
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


        [HttpPost("addproduct")]
        public async Task<IActionResult> AddProductAsync([FromForm] ProductDto productDto)
        {
            try
            {
                var response = await _productService.AddProductAsync(productDto);

                if (response.IsSuccess)
                {
                    return Ok(response);
                }

                return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<Product>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Unknown error",
                    ResponseObject = new Product()
                }); ;
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

        [HttpDelete("deleteproduct/{productId}")]
        public async Task<IActionResult> DeleteProductAsync([FromRoute] Guid productId)
        {
            try
            {
                var response = await _productService.DeleteProductAsync(productId);

                if (response.IsSuccess)
                {
                    return Ok(response);
                }

                return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<Product>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Unknown error",
                    ResponseObject = new Product()
                }); ;
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


        [HttpPut("updateproduct")]
        public async Task<IActionResult> UpdateProductAsync([FromForm] ProductDto productDto)
        {
            try
            {
                var response = await _productService.UpdateProductAsync(productDto);

                if (response.IsSuccess)
                {
                    return Ok(response);
                }

                return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<Product>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Unknown error",
                    ResponseObject = new Product()
                }); ;
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


        [HttpGet("getproduct/{productId}")]
        public async Task<IActionResult> GetProductByProductIdAsync([FromRoute] Guid productId)
        {
            try
            {
                var response = await _productService.GetProductByProductIdAsync(productId);

                if (response.IsSuccess)
                {
                    return Ok(response);
                }

                return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<Product>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Unknown error",
                    ResponseObject = new Product()
                }); ;
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
