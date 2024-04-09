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
        public IActionResult AddProduct([FromForm] ProductDto productDto)
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

                Product product = SaveProductImages(productDto);
                
                
                
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
                var productImages = _productImagesRepository.GetProductImagesByProductId(productId);
                foreach(var p in productImages)
                {
                    DeleteExistingProductImage(p.ProductImageUrl);
                }
                _productImagesRepository.RemoveImagesByProductId(productId);
                var productItemsImages = _productItemRepository.GetAllProductItemsByProductId(productId);
                foreach(var i in productItemsImages)
                {
                    DeleteExistingItemImage(i.ProducItemImageUrl);
                }
                Product deletedProduct = _productRepository.DeleteProductById(productId);
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
        public IActionResult UpdateProduct([FromForm] ProductDto productDto)
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
                Product updatedProduct = UpdateProductImages(productDto);
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

        //
        private Product SaveProductImages(ProductDto productDto)
        {
            try
            {
                Product product = ConvertFromDto.ConvertFromProductDto_Add(productDto);
                Product savedProduct = _productRepository.AddProduct(product);
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
                    p.ImageUrl = SaveProductImage(p);
                    _productImagesRepository.AddProductImages(new ProductImages
                    {
                        ProductId = new Guid(p.ProductId),
                        ProductImageUrl = p.ImageUrl
                    });
                }
                savedProduct.ProductImages = _productImagesRepository.GetProductImagesByProductId(savedProduct.Id).ToList();
                return savedProduct;
            }
            catch (Exception)
            {
                throw;
            } 
        }

        private Product UpdateProductImages(ProductDto productDto)
        {
            try
            {
                if (productDto.Id == null)
                {
                    throw new NullReferenceException("You must enter product id");
                }
                Product product = _productRepository.GetProductById(new Guid(productDto.Id));
                Product updatedProduct = 
                    _productRepository.UpdateProduct(ConvertFromDto.ConvertFromProductDto_Update(productDto));
                if(product == null)
                {
                    return null;
                }
                IEnumerable<ProductImages> productImages = 
                    _productImagesRepository.GetProductImagesByProductId(product.Id);
                
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
                    p.ImageUrl = SaveProductImage(p);
                    _productImagesRepository.AddProductImages(new ProductImages
                    {
                        ProductId = new Guid(p.ProductId),
                        ProductImageUrl = p.ImageUrl
                    });
                }
                updatedProduct.ProductImages = _productImagesRepository.GetProductImagesByProductId(updatedProduct.Id).ToList();
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
