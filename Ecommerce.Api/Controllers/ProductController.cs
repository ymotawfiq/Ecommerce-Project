using Ecommerce.Data.DTOs;
using Ecommerce.Data.Extensions;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Repository.Repositories.ProductCategoryRepository;
using Ecommerce.Repository.Repositories.ProductImagesRepository;
using Ecommerce.Repository.Repositories.ProductItemRepository;
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
        private readonly IProductImages _productImagesRepository;
        private readonly IProductItem _productItemRepository;
        public ProductController(IProduct _productRepository, IWebHostEnvironment _webHostEnvironment,
            IProductCategory _productCategoryRepository, IProductImages _productImagesRepository,
            IProductItem _productItemRepository)
        {
            this._productRepository = _productRepository;
            this._webHostEnvironment = _webHostEnvironment;
            this._productCategoryRepository = _productCategoryRepository;
            this._productImagesRepository = _productImagesRepository;
            this._productItemRepository = _productItemRepository;
        }

        [HttpGet("allproducts")]
        public async Task<IActionResult> GetAllProductsAsync()
        {
            try
            {
                var products = await _productRepository.GetAllProductsAsync();
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
        public async Task<IActionResult> GetAllProductsAsync([FromRoute] Guid categoryId)
        {
            try
            {
                var products = await _productRepository.GetProductsByCategoryIdAsync(categoryId);
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
        public async Task<IActionResult> AddProductAsync([FromForm] ProductDto productDto)
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
                if (await _productCategoryRepository.GetCategoryByIdAsync(productDto.CategoryId) == null)
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



                Product product = await SaveProductImagesAsync(productDto);

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
        public async Task<IActionResult> DeleteProductAsync([FromRoute] Guid productId)
        {
            try
            {
                Product product = await _productRepository.GetProductByIdAsync(productId);
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
                Product deletedProduct = await _productRepository.DeleteProductByIdAsync(productId);

                var productImages = await _productImagesRepository.GetProductImagesByProductIdAsync(productId);
                foreach(var p in productImages)
                {
                    DeleteExistingProductImage(p.ProductImageUrl);
                }
                _productImagesRepository.RemoveImagesByProductIdAsync(productId);
                var productItemsImages = await _productItemRepository.GetAllProductItemsByProductIdAsync(productId);
                foreach(var i in productItemsImages)
                {
                    DeleteExistingItemImage(i.ProducItemImageUrl);
                }
                return StatusCode(StatusCodes.Status200OK, new ApiResponse<Product>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Product deleted successfully",
                    ResponseObject = deletedProduct
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


        [HttpPut("updateproduct")]
        public async Task<IActionResult> UpdateProductAsync([FromForm] ProductDto productDto)
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
                Product updatedProduct = await UpdateProductImagesAsync(productDto);
                if (updatedProduct == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<Product>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = $"No products found with id ({productDto.Id})",
                        ResponseObject = new Product()
                    });
                }

                return StatusCode(StatusCodes.Status200OK, new ApiResponse<Product>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Product updated successfully",
                    ResponseObject = updatedProduct
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
        public async Task<IActionResult> GetProductByProductIdAsync([FromRoute] Guid productId)
        {
            try
            {
                Product product = await _productRepository.GetProductByIdAsync(productId);
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
                Product ExistingProduct = await _productRepository.GetProductByIdAsync(productId);
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

        //
        private async Task<Product> SaveProductImagesAsync(ProductDto productDto)
        {
            try
            {
                if (productDto.Image == null)
                {
                    throw new NullReferenceException("You must enter image url");
                }
                Product product = ConvertFromDto.ConvertFromProductDto_Add(productDto);
                Product savedProduct = await _productRepository.AddProductAsync(product);
                List<ProductImageDto> productImageDtos = new();
                foreach (var i in productDto.Image)
                {
                    productImageDtos.Add(new ProductImageDto
                    {
                        Image = i,
                        ProductId = savedProduct.Id.ToString()
                    });
                }

                foreach (var p in productImageDtos)
                {
                    if (p.ProductId == null)
                    {
                        throw new NullReferenceException("You must enter product id");
                    }
                    p.ImageUrl = SaveProductImage(p);
                    await _productImagesRepository.AddProductImagesAsync(new ProductImages
                    {
                        ProductId = new Guid(p.ProductId),
                        ProductImageUrl = p.ImageUrl
                    });
                }
                //if (productDto.Id == null)
                //{
                //    throw new NullReferenceException("You must enter product id");
                //}
                savedProduct.ProductImages = (await _productImagesRepository
                    .GetProductImagesByProductIdAsync(new Guid(productDto.Id))).ToList();
                return savedProduct;
            }
            catch (Exception)
            {
                throw;
            } 
        }

        private async Task<Product> UpdateProductImagesAsync(ProductDto productDto)
        {
            try
            {
                if (productDto.Id == null)
                {
                    throw new NullReferenceException("You must enter product id");
                }
                else if (productDto.Image == null)
                {
                    throw new NullReferenceException("You must enter image url");
                }
                Product product = await _productRepository.GetProductByIdAsync(new Guid(productDto.Id));
                Product updatedProduct = 
                    await _productRepository.UpdateProductAsync
                    (ConvertFromDto.ConvertFromProductDto_Update(productDto));
                if(product == null)
                {
                    return null;
                }
                IEnumerable<ProductImages> productImages = 
                    await _productImagesRepository.GetProductImagesByProductIdAsync(product.Id);
                
                foreach(var p in productImages)
                {
                    DeleteExistingProductImage(p.ProductImageUrl);
                }
                List<ProductImageDto> productImageDtos = new();
                foreach (var i in productDto.Image)
                {
                    productImageDtos.Add(new ProductImageDto
                    {
                        Image = i,
                        ProductId = product.Id.ToString()
                    });
                }

                foreach (var p in productImageDtos)
                {
                    if (p.ProductId == null)
                    {
                        throw new NullReferenceException("You must enter product id");
                    }
                    p.ImageUrl = SaveProductImage(p);
                    await _productImagesRepository.AddProductImagesAsync(new ProductImages
                    {
                        ProductId = new Guid(p.ProductId),
                        ProductImageUrl = p.ImageUrl
                    });
                }
                updatedProduct.ProductImages = (await _productImagesRepository
                    .GetProductImagesByProductIdAsync(new Guid(productDto.Id))).ToList();
                
                return updatedProduct;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Save Image Url
        private string SaveProductImage(ProductImageDto productImageDto)
        {
            string uniqueFileName = null;
            if (productImageDto.Image != null)
            {
                //if (productImageDto.ImageUrl != null)
                //{
                //    DeleteExistingProductImage(productImageDto.ImageUrl);
                //}
                string path = @"D:\my_source_code\C sharp\EcommerceProjectSolution\Ecommerce.Client\wwwroot";

                string uploadsFolder = Path.Combine(path, "Images/Products");
                if (!System.IO.Directory.Exists(uploadsFolder))
                {
                    System.IO.Directory.CreateDirectory(uploadsFolder);
                }
                uniqueFileName = Guid.NewGuid().ToString().Substring(0, 8) + "_" + productImageDto.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    productImageDto.Image.CopyTo(fileStream);
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

        private void DeleteExistingItemImage(string imageUrl)
        {
            string path = @"D:/my_source_code/C sharp/EcommerceProjectSolution/Ecommerce.Client/wwwroot/Images/ProductItems/";
            string existingImage = Path.Combine(path, $"{imageUrl}");
            if (System.IO.File.Exists(existingImage))
            {
                System.IO.File.Delete(existingImage);
            }
        }
    }
}
