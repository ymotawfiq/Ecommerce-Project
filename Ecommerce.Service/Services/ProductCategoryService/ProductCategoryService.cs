

using Ecommerce.Data.DTOs;
using Ecommerce.Data.Extensions;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Repository.Repositories.ProductCategoryRepository;
using Ecommerce.Repository.Repositories.ProductImagesRepository;
using Ecommerce.Repository.Repositories.ProductRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.Service.Services.ProductCategoryService
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategory _cateegoryRepository;
        private readonly IProduct _productRepository;
        private readonly IProductImages _productImagesRepository;
        public ProductCategoryService(IProductCategory _cateegoryRepository, IProduct _productRepository,
            IProductImages _productImagesRepository)
        {
            this._cateegoryRepository = _cateegoryRepository;
            this._productRepository = _productRepository;
            this._productImagesRepository = _productImagesRepository;
        }
        public async Task<ApiResponse<ProductCategory>> AddCategoryAsync(ProductCategoryDto productCategoryDto)
        {
            if (productCategoryDto == null)
            {
                return new ApiResponse<ProductCategory>
                {
                    IsSuccess = false,
                    StatusCode = 400,
                    Message = "Input must not be null",
                    ResponseObject = new ProductCategory()
                };
            }
            if (productCategoryDto.ParentCategoryId != null)
            {
                if (!productCategoryDto.ParentCategoryId.IsNullOrEmpty()
                     && await _cateegoryRepository.GetCategoryByIdAsync
                     (new Guid(productCategoryDto.ParentCategoryId)) == null)
                {
                    return new ApiResponse<ProductCategory>
                    {
                        IsSuccess = false,
                        StatusCode = 400,
                        Message = $"Parent category with id ({productCategoryDto.ParentCategoryId}) not found please choose exixting category ",
                        ResponseObject = new ProductCategory()
                    };
                }
                else if (productCategoryDto.Id == productCategoryDto.ParentCategoryId)
                {
                    return new ApiResponse<ProductCategory>
                    {
                        IsSuccess = false,
                        StatusCode = 400,
                        Message = $"Category id and Parent category id must not be same",
                        ResponseObject = new ProductCategory()
                    };
                }
            }
            ProductCategory newCategory = await _cateegoryRepository.AddNewCategoryAsync(
                ConvertFromDto.ConvertFromProductCategoryDto_Add(productCategoryDto));
            return new ApiResponse<ProductCategory>
            {
                IsSuccess = true,
                StatusCode = 201,
                Message = "Category created successfully",
                ResponseObject = newCategory
            };
        }

        public async Task<ApiResponse<ProductCategory>> DeleteCategoryByCategoryIdAsync(Guid categoryId)
        {
            if (categoryId.ToString().IsNullOrEmpty())
            {
                return new ApiResponse<ProductCategory>
                {
                    IsSuccess = false,
                    StatusCode = 400,
                    Message = "You must enter category id",
                    ResponseObject = new ProductCategory()
                };
            }
            ProductCategory productCategory = await _cateegoryRepository.GetCategoryByIdAsync(categoryId);
            if (productCategory == null)
            {
                return new ApiResponse<ProductCategory>
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = $"Category with id ({categoryId}) not exists",
                    ResponseObject = new ProductCategory()
                };
            }
            var products = await _productRepository.GetProductsByCategoryIdAsync(categoryId);
            foreach (var p in products)
            {
                var productImages = await _productImagesRepository.GetProductImagesByProductIdAsync(p.Id);
                foreach (var k in productImages)
                {
                    DeleteExistingProductImage(k.ProductImageUrl);
                }
            }
            ProductCategory deletedCategory = await _cateegoryRepository.DeleteCategoryAsync(categoryId);
            return new ApiResponse<ProductCategory>
            {
                IsSuccess = true,
                StatusCode = 200,
                Message = "Category deleted successfully",
                ResponseObject = deletedCategory
            };
        }

        public async Task<ApiResponse<IEnumerable<ProductCategory>>> GetAllCategoriesAsync()
        {
            var products = await _cateegoryRepository.GetAllCategoriesAsync();
            if (products.ToList().Count == 0)
            {
                return new ApiResponse<IEnumerable<ProductCategory>>
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Message = "No categories found",
                    ResponseObject = products
                };
            }
            return new ApiResponse<IEnumerable<ProductCategory>>
            {
                IsSuccess = true,
                StatusCode = 200,
                Message = "Categories found successfully",
                ResponseObject = products
            };
        }

        public async Task<ApiResponse<ProductCategory>> GetCategoryByIdAsync(Guid categoryId)
        {
            if (categoryId.ToString().IsNullOrEmpty())
            {
                return new ApiResponse<ProductCategory>
                {
                    IsSuccess = false,
                    StatusCode = 400,
                    Message = "You must enter category id",
                    ResponseObject = new ProductCategory()
                };
            }
            IEnumerable<Product> products = await _productRepository.GetProductsByCategoryIdAsync(categoryId);
            ProductCategory category = await _cateegoryRepository.GetCategoryByIdAsync(categoryId);
            category.Products = new HashSet<Product>(products);
            if (category == null)
            {
                return new ApiResponse<ProductCategory>
                {
                    IsSuccess = false,
                    StatusCode = 400,
                    Message = $"No categories found with id ({categoryId})",
                    ResponseObject = new ProductCategory()
                };
            }
            return new ApiResponse<ProductCategory>
            {
                IsSuccess = true,
                StatusCode = 200,
                Message = $"Category witd id ({categoryId}) found successfully",
                ResponseObject = category
            };
        }

        public async Task<ApiResponse<ProductCategory>> UpdateCategoryAsync(ProductCategoryDto productCategoryDto)
        {
            if (productCategoryDto.Id == null)
            {
                return new ApiResponse<ProductCategory>
                {
                    IsSuccess = false,
                    StatusCode = 400,
                    Message = "You must enter category id",
                    ResponseObject = new ProductCategory()
                };
            }
            if (productCategoryDto.ParentCategoryId != null)
            {
                if (!productCategoryDto.ParentCategoryId.IsNullOrEmpty()
                     && await _cateegoryRepository.GetCategoryByIdAsync
                     (new Guid(productCategoryDto.ParentCategoryId)) == null)
                {
                    return new ApiResponse<ProductCategory>
                    {
                        IsSuccess = false,
                        StatusCode = 400,
                        Message = $"Parent category with id ({productCategoryDto.ParentCategoryId}) not found please choose exixting category ",
                        ResponseObject = new ProductCategory()
                    };
                }
                else if (productCategoryDto.Id == productCategoryDto.ParentCategoryId)
                {
                    return new ApiResponse<ProductCategory>
                    {
                        IsSuccess = false,
                        StatusCode = 400,
                        Message = $"Category id and Parent category id must not be same",
                        ResponseObject = new ProductCategory()
                    };
                }
            }
            ProductCategory category = await _cateegoryRepository.UpdateCategoryAsync(
                ConvertFromDto.ConvertFromProductCategoryDto_Update(productCategoryDto));
            return new ApiResponse<ProductCategory>
            {
                IsSuccess = true,
                Message = "Category updated successfully",
                StatusCode = 200,
                ResponseObject = category
            };
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
