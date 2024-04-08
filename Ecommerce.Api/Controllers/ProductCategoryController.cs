using Ecommerce.Data.DTOs;
using Ecommerce.Data.Extensions;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Repository.Repositories.ProductCategoryRepository;
using Ecommerce.Repository.Repositories.ProductImagesRepository;
using Ecommerce.Repository.Repositories.ProductRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategory _cateegoryRepository;
        private readonly IProduct _productRepository;
        private readonly IProductImages _productImagesRepository;
        public ProductCategoryController(IProductCategory _cateegoryRepository, IProduct _productRepository,
            IProductImages _productImagesRepository)
        {
            this._cateegoryRepository = _cateegoryRepository;
            this._productRepository = _productRepository;
            this._productImagesRepository = _productImagesRepository;
        }

        [HttpGet("allcategories")]
        public IActionResult GetAllCategories()
        {
            try
            {
                var products = _cateegoryRepository.GetAllCategories();
                if (products.ToList().Count == 0)
                {
                    return StatusCode(StatusCodes.Status200OK, new ApiResponse<IEnumerable<ProductCategory>>
                    {
                        IsSuccess = true,
                        StatusCode = 200,
                        Message = "No categories found",
                        ResponseObject = products
                    });
                }
                return StatusCode(StatusCodes.Status200OK, new ApiResponse<IEnumerable<ProductCategory>>
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Message = "Categories found successfully",
                    ResponseObject = products
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<IEnumerable<ProductCategory>>
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Message = ex.Message,
                    ResponseObject = new List<ProductCategory>()
                });
            }
        }

        [HttpPost("addcategory")]
        public IActionResult AddCategory([FromBody] ProductCategoryDto productCategoryDto)
        {
            try
            {
                if (productCategoryDto == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<ProductCategory>
                    {
                        IsSuccess = false,
                        StatusCode = 400,
                        Message = "Input must not be null",
                        ResponseObject = new ProductCategory()
                    });
                }
                if (productCategoryDto.ParentCategoryId != null)
                {
                    if (!productCategoryDto.ParentCategoryId.IsNullOrEmpty()
                         && _cateegoryRepository.GetCategoryById(new Guid(productCategoryDto.ParentCategoryId)) == null)
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<ProductCategory>
                        {
                            IsSuccess = false,
                            StatusCode = 400,
                            Message = $"Parent category with id ({productCategoryDto.ParentCategoryId}) not found please choose exixting category ",
                            ResponseObject = new ProductCategory()
                        });
                    }
                    else if (productCategoryDto.Id == productCategoryDto.ParentCategoryId)
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<ProductCategory>
                        {
                            IsSuccess = false,
                            StatusCode = 400,
                            Message = $"Category id and Parent category id must not be same",
                            ResponseObject = new ProductCategory()
                        });
                    }
                }
                ProductCategory newCategory = _cateegoryRepository.AddNewCategory(
                    ConvertFromDto.ConvertFromProductCategoryDto_Add(productCategoryDto));
                return StatusCode(StatusCodes.Status201Created, new ApiResponse<ProductCategory>
                {
                    IsSuccess = true,
                    StatusCode = 201,
                    Message = "Category created successfully",
                    ResponseObject = newCategory
                });
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

        [HttpGet("getcategorybyid/{categoryId}")]
        public IActionResult GetCategoryById([FromRoute] Guid categoryId)
        {
            try
            {
                if (categoryId.ToString().IsNullOrEmpty())
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<ProductCategory>
                    {
                        IsSuccess = false,
                        StatusCode = 400,
                        Message = "You must enter category id",
                        ResponseObject = new ProductCategory()
                    });
                }
                IEnumerable<Product> products = _productRepository.GetProductsByCategoryId(categoryId);
                ProductCategory category = _cateegoryRepository.GetCategoryById(categoryId);
                category.Products = new HashSet<Product>(products);
                if(category == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<ProductCategory>
                    {
                        IsSuccess = false,
                        StatusCode = 400,
                        Message = $"No categories found with id ({categoryId})",
                        ResponseObject = new ProductCategory()
                    });
                }
                return StatusCode(StatusCodes.Status200OK, new ApiResponse<ProductCategory>
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Message = $"Category witd id ({categoryId}) found successfully",
                    ResponseObject = category
                });
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

        [HttpPut("updatecategory")]
        public IActionResult UpdateCategory(ProductCategoryDto productCategoryDto)
        {
            try
            {
                if(productCategoryDto.Id == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<ProductCategory>
                    {
                        IsSuccess = false,
                        StatusCode = 400,
                        Message = "You must enter category id",
                        ResponseObject = new ProductCategory()
                    });
                }
                if (productCategoryDto.ParentCategoryId != null)
                {
                    if (!productCategoryDto.ParentCategoryId.IsNullOrEmpty()
                         && _cateegoryRepository.GetCategoryById(new Guid(productCategoryDto.ParentCategoryId)) == null)
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<ProductCategory>
                        {
                            IsSuccess = false,
                            StatusCode = 400,
                            Message = $"Parent category with id ({productCategoryDto.ParentCategoryId}) not found please choose exixting category ",
                            ResponseObject = new ProductCategory()
                        });
                    }
                    else if(productCategoryDto.Id == productCategoryDto.ParentCategoryId)
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<ProductCategory>
                        {
                            IsSuccess = false,
                            StatusCode = 400,
                            Message = $"Category id and Parent category id must not be same",
                            ResponseObject = new ProductCategory()
                        });
                    }
                }
                ProductCategory category = _cateegoryRepository.UpdateCategory(
                    ConvertFromDto.ConvertFromProductCategoryDto_Update(productCategoryDto));
                return StatusCode(StatusCodes.Status200OK, new ApiResponse<ProductCategory>
                {
                    IsSuccess = true,
                    Message = "Category updated successfully",
                    StatusCode = 200,
                    ResponseObject = category
                });
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

        [HttpDelete("deletecategory/{categoryId}")]
        public IActionResult DeleteCategoryByCategoryId([FromRoute] Guid categoryId)
        {
            try
            {
                if (categoryId.ToString().IsNullOrEmpty())
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<ProductCategory>
                    {
                        IsSuccess = false,
                        StatusCode = 400,
                        Message = "You must enter category id",
                        ResponseObject = new ProductCategory()
                    });
                }
                var products = _productRepository.GetProductsByCategoryId(categoryId);
                foreach (var p in products)
                {
                    var productImages = _productImagesRepository.GetProductImagesByProductId(p.Id);
                    foreach(var k in productImages)
                    {
                        DeleteExistingProductImage(k.ProductImageUrl);
                    }
                }
                ProductCategory deletedCategory = _cateegoryRepository.DeleteCategory(categoryId);
                return StatusCode(StatusCodes.Status200OK, new ApiResponse<ProductCategory>
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Message = "Category deleted successfully",
                    ResponseObject = deletedCategory
                });
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
