using Ecommerce.Data.DTOs;
using Ecommerce.Data.Extensions;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Repository.Repositories.ProductCategoryRepository;
using Ecommerce.Repository.Repositories.ProductRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProduct _productRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IProductCategory _productCategoryRepository;
        public ProductController(IProduct _productRepository, IWebHostEnvironment _webHostEnvironment,
            IProductCategory _productCategoryRepository)
        {
            this._productRepository = _productRepository;
            this._webHostEnvironment = _webHostEnvironment;
            this._productCategoryRepository = _productCategoryRepository;
        }

        [HttpGet("allproducts")]
        public IActionResult GetAllProducts()
        {
            try
            {
                var products = _productRepository.GetAllProducts();
                if (products.ToList().Count==0)
                {
                    return StatusCode(StatusCodes.Status200OK, new ApiResponse<IEnumerable<Product>>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = "No products found",
                        ResponseObject = products
                    });
                }
                return StatusCode(StatusCodes.Status200OK, new ApiResponse<IEnumerable<Product>>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Products found successfully",
                    ResponseObject = products
                });
            }
            catch(Exception ex)
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

        [HttpGet("allproductsbycategoryid/{categoryId}")]
        public IActionResult GetAllProducts([FromRoute] Guid categoryId)
        {
            try
            {
                var products = _productRepository.GetProductsByCategoryId(categoryId);
                if (products.ToList().Count == 0)
                {
                    return StatusCode(StatusCodes.Status200OK, new ApiResponse<IEnumerable<Product>>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = "No products found",
                        ResponseObject = products
                    });
                }
                return StatusCode(StatusCodes.Status200OK, new ApiResponse<IEnumerable<Product>>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Products founded successfully",
                    ResponseObject = products
                });
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
        public async Task<IActionResult> AddProduct([FromForm] ProductDto productDto)
        {
            try
            {
                if (productDto == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<Product>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Input must not be null",
                    });
                }
                if (_productCategoryRepository.GetCategoryById(productDto.CategoryId) == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<Product>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = $"Category with id ({productDto.CategoryId}) not found",
                    });
                }
                
                if (productDto.Image == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<Product>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "You must enter product image",
                    });
                }
                string productImage = SaveProductImage(productDto);
                productDto.ProductImageUrl = productImage;
                var product = _productRepository.AddProduct(ConvertFromDto.ConvertFromProductDto_Add(productDto));
                return StatusCode(StatusCodes.Status201Created, new ApiResponse<Product>
                {
                    StatusCode = 201,
                    IsSuccess = true,
                    Message = "Product saved successfully",
                    ResponseObject = product
                });

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
        public IActionResult DeleteProduct([FromRoute] Guid productId)
        {
            try
            {
                Product product = _productRepository.GetProductById(productId);
                if(product == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<Product>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = $"No products found with id ({productId})",
                        ResponseObject = new Product()
                    });
                }
                DeleteExistingProductImage(product.ProductImageUrl);
                Product deletedProduct = _productRepository.DeleteProductById(productId);
                return StatusCode(StatusCodes.Status200OK, new ApiResponse<Product>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Product deleted successfully",
                    ResponseObject = deletedProduct
                });
            }
            catch(Exception ex)
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
        public IActionResult updateProduct([FromForm] ProductDto productDto)
        {
            try
            {
                if (productDto == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<Product>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Input must not be null",
                        ResponseObject = new Product()
                    });
                }
                if(!productDto.Id.IsNullOrEmpty() && productDto.Id != null)
                {
                    if (productDto.Image != null)
                    {
                        productDto.ProductImageUrl = productDto.Image.FileName;
                        Product oldProduct = _productRepository.GetProductById(new Guid(productDto.Id));
                        DeleteExistingProductImage(oldProduct.ProductImageUrl);
                        string productImage = SaveProductImage(productDto);
                        productDto.ProductImageUrl = productImage;
                        var updatedProduct = _productRepository.UpdateProduct(ConvertFromDto.ConvertFromProductDto_Update(productDto));
                        return StatusCode(StatusCodes.Status200OK, new ApiResponse<Product>
                        {
                            StatusCode = 200,
                            IsSuccess = true,
                            Message = "Product updaed successfully",
                            ResponseObject = updatedProduct
                        });
                    }
                }
               

                return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<Product>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "You must enter product image",
                    ResponseObject = new Product()
                });

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
        public IActionResult GetProductByProductId([FromRoute] Guid productId)
        {
            try
            {
                Product product = _productRepository.GetProductById(productId);
                if (product == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<Product>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = $"No products found with id ({productId})",
                        ResponseObject = new Product()
                    });
                }
                Product ExistingProduct = _productRepository.GetProductById(productId);
                return StatusCode(StatusCodes.Status200OK, new ApiResponse<Product>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Product founded successfully",
                    ResponseObject = ExistingProduct
                });
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


        // Save Image Url
        private string SaveProductImage(ProductDto productDto)
        {
            string uniqueFileName = null;
            if (productDto.Image != null)
            {
                if (productDto.ProductImageUrl != null)
                {
                    DeleteExistingProductImage(productDto.ProductImageUrl);
                }
                string path = @"D:\my_source_code\C sharp\EcommerceProjectSolution\Ecommerce.Client\wwwroot";

                string uploadsFolder = Path.Combine(path, "Images/Products");
                if (!System.IO.Directory.Exists(uploadsFolder))
                {
                    System.IO.Directory.CreateDirectory(uploadsFolder);
                }
                uniqueFileName = Guid.NewGuid().ToString().Substring(0, 8) + "_" + productDto.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    productDto.Image.CopyTo(fileStream);
                    fileStream.Flush();
                }
            }
            return uniqueFileName;
        }

        private void DeleteExistingProductImage(string imageUrl)
        {
            string path = @"D:/my_source_code/C sharp/EcommerceProjectSolution/Ecommerce.Client/wwwroot/Images/Products/";
            string existingImage = Path.Combine(path, $"{imageUrl}");
            if (System.IO.File.Exists(existingImage))
            {
                System.IO.File.Delete(existingImage);
            }
        }
    }
}
