

using Ecommerce.Data.DTOs;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;

namespace Ecommerce.Service.Services.PromotionCategoryService
{
    public interface IPromotionCategoryService
    {
        Task<ApiResponse<IEnumerable<PromotionCategory>>> GetAllPromotionCategoriesAsync();
        Task<ApiResponse<IEnumerable<PromotionCategory>>> GetAllPromotionCategoriesByCategoryIdAsync(Guid categoryId);
        Task<ApiResponse<IEnumerable<PromotionCategory>>> GetAllPromotionCategoriesByPromotionIdAsync(Guid promotionId);
        Task<ApiResponse<PromotionCategory>> AddPromotionCategoryAsync(PromotionCategoryDto promotionCategoryDto);
        Task<ApiResponse<PromotionCategory>> UpdatePromotionCategoryAsync(PromotionCategoryDto promotionCategoryDto);
        Task<ApiResponse<PromotionCategory>> GetPromotionCategoryByIdAsync(Guid promotionCategoryId);
        Task<ApiResponse<PromotionCategory>> DeletePromotionCategoryByIdAsync(Guid promotionCategoryId);
    }
}
