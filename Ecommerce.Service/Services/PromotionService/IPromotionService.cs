using Ecommerce.Data.DTOs;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;


namespace Ecommerce.Service.Services.PromotionService
{
    public interface IPromotionService
    {
        Task<ApiResponse<IEnumerable<Promotion>>> GetAllPromotionsAsync();
        Task<ApiResponse<Promotion>> AddPromotionAsync(PromotionDto promotionDto);
        Task<ApiResponse<Promotion>> UpdatePromotionAsync(PromotionDto promotionDto);
        Task<ApiResponse<Promotion>> GetPromotionByIdAsync(Guid promotionId);
        Task<ApiResponse<Promotion>> DeletePromotionByIdAsync(Guid promotionId);
    }
}
