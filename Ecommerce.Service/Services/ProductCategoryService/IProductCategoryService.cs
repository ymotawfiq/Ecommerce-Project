

using Ecommerce.Data.DTOs;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;

namespace Ecommerce.Service.Services.ProductCategoryService
{
    public interface IProductCategoryService
    {
        Task<ApiResponse<IEnumerable<ProductCategory>>> GetAllCategoriesAsync();
        Task<ApiResponse<ProductCategory>> AddCategoryAsync(ProductCategoryDto productCategoryDto);
        Task<ApiResponse<ProductCategory>> GetCategoryByIdAsync(Guid categoryId);
        Task<ApiResponse<ProductCategory>> UpdateCategoryAsync(ProductCategoryDto productCategoryDto);
        Task<ApiResponse<ProductCategory>> DeleteCategoryByCategoryIdAsync(Guid categoryId);

    }
}
