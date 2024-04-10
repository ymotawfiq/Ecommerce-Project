using Ecommerce.Data.DTOs;
using Ecommerce.Data.Extensions;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Repository.Repositories.ProductCategoryRepository;
using Ecommerce.Repository.Repositories.ProductImagesRepository;
using Ecommerce.Repository.Repositories.ProductItemRepository;
using Ecommerce.Repository.Repositories.ProductRepository;


namespace Ecommerce.Service.Services.ProductService.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IProduct _productRepository;
        private readonly IProductCategory _productCategoryRepository;
        private readonly IProductImages _productImagesRepository;
        private readonly IProductItem _productItemRepository;
        public ProductService(IProduct _productRepository,
            IProductCategory _productCategoryRepository, IProductImages _productImagesRepository,
            IProductItem _productItemRepository)
        {
            this._productRepository = _productRepository;
            this._productCategoryRepository = _productCategoryRepository;
            this._productImagesRepository = _productImagesRepository;
            this._productItemRepository = _productItemRepository;
        }



        public async Task<ApiResponse<Product>> AddProductAsync(ProductDto productDto)
        {
            try
            {
                if (productDto == null)
                {
                    return new ApiResponse<Product>
                    {
                        IsSuccess = false,
                        Message = "Input must not be null",
                        StatusCode = 400,
                        ResponseObject = new Product()
                    };
                }
                productDto.Id = new Guid().ToString();
                ProductCategory productCategory = await _productCategoryRepository
                    .GetCategoryByIdAsync(productDto.CategoryId);
                if (productCategory == null)
                {
                    return new ApiResponse<Product>
                    {
                        IsSuccess = false,
                        StatusCode = 400,
                        Message = $"No categories founded with id ({productDto.CategoryId})",
                        ResponseObject = new Product()
                    };
                }
                if (productDto.Image == null)
                {
                    return new ApiResponse<Product>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "You must enter product image",
                    };
                }
                Product product = await SaveProductImagesAsync(productDto);

                return new ApiResponse<Product>
                {
                    StatusCode = 201,
                    IsSuccess = true,
                    Message = "Product saved successfully",
                    ResponseObject = product
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Product>
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = ex.Message,
                    ResponseObject = new Product()
                };
            }
        }

        public async Task<ApiResponse<Product>> UpdateProductAsync(ProductDto productDto)
        {
            try
            {
                if (productDto == null)
                {
                    return new ApiResponse<Product>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Input must not be null",
                        ResponseObject = new Product()
                    };
                }
                Product updatedProduct = await UpdateProductImagesAsync(productDto);
                if (updatedProduct == null)
                {
                    return new ApiResponse<Product>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = $"No products found with id ({productDto.Id})",
                        ResponseObject = new Product()
                    };
                }

                return new ApiResponse<Product>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Product updated successfully",
                    ResponseObject = updatedProduct
                };

            }
            catch (Exception ex)
            {
                return new ApiResponse<Product>
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = ex.Message,
                    ResponseObject = new Product()
                };
            }
        }

        public async Task<ApiResponse<Product>> DeleteProductAsync(Guid productId)
        {
            try
            {
                Product product = await _productRepository.GetProductByIdAsync(productId);
                if (product == null)
                {
                    return new ApiResponse<Product>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = $"No products found with id ({productId})",
                        ResponseObject = new Product()
                    };
                }

                var productImages = await _productImagesRepository.GetProductImagesByProductIdAsync(productId);
                foreach (var p in productImages)
                {
                    DeleteExistingProductImage(p.ProductImageUrl);
                }
                await _productImagesRepository.RemoveImagesByProductIdAsync(productId);
                var productItemsImages = await _productItemRepository.GetAllProductItemsByProductIdAsync(productId);
                foreach (var i in productItemsImages)
                {
                    DeleteExistingItemImage(i.ProducItemImageUrl);
                }
                Product deletedProduct = await _productRepository.DeleteProductByIdAsync(productId);

                return new ApiResponse<Product>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Product deleted successfully",
                    ResponseObject = deletedProduct
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Product>
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = ex.Message,
                    ResponseObject = new Product()
                };
            }
        }

        public async Task<ApiResponse<Product>> GetProductByProductIdAsync(Guid productId)
        {
            try
            {
                Product product = await _productRepository.GetProductByIdAsync(productId);
                if (product == null)
                {
                    return new ApiResponse<Product>
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = $"No products found with id ({productId})",
                        ResponseObject = new Product()
                    };
                }
                Product ExistingProduct = await _productRepository.GetProductByIdAsync(productId);
                return new ApiResponse<Product>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Product founded successfully",
                    ResponseObject = ExistingProduct
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Product>
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = ex.Message,
                    ResponseObject = new Product()
                };
            }
        }

        public async Task<ApiResponse<IEnumerable<Product>>> GetAllProductsAsync()
        {

            var products = await _productRepository.GetAllProductsAsync();
            if (products.ToList().Count == 0)
            {
                return new ApiResponse<IEnumerable<Product>>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "No products found",
                    ResponseObject = products
                };
            }
            return new ApiResponse<IEnumerable<Product>>
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Products found successfully",
                ResponseObject = products
            };
            
        }

        public async Task<ApiResponse<IEnumerable<Product>>> GetAllProductsByCategoryIdAsync(Guid categoryId)
        {

            var products = await _productRepository.GetProductsByCategoryIdAsync(categoryId);
            if (products.ToList().Count == 0)
            {
                return new ApiResponse<IEnumerable<Product>>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "No products found",
                    ResponseObject = products
                };
            }
            return new ApiResponse<IEnumerable<Product>>
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Products founded successfully",
                ResponseObject = products
            };
            
        }

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
                if (product == null)
                {
                    return null;
                }
                IEnumerable<ProductImages> productImages =
                    await _productImagesRepository.GetProductImagesByProductIdAsync(product.Id);

                foreach (var p in productImages)
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
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
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
            if (File.Exists(existingImage))
            {
                File.Delete(existingImage);
            }
        }

        private void DeleteExistingItemImage(string imageUrl)
        {
            string path = @"D:/my_source_code/C sharp/EcommerceProjectSolution/Ecommerce.Client/wwwroot/Images/ProductItems/";
            string existingImage = Path.Combine(path, $"{imageUrl}");
            if (File.Exists(existingImage))
            {
                File.Delete(existingImage);
            }
        }

        
    }
}




