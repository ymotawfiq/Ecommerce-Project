

using Ecommerce.Data.DTOs;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;

namespace Ecommerce.Service.Services.VariationOptionService
{
    public interface IVariationOptionsService
    {
        Task<ApiResponse<IEnumerable<VariationOptions>>> GetAllVariationOptionsAsync();
        Task<ApiResponse<IEnumerable<VariationOptions>>> GetAllVariationOptionsByVariationIdAsync(Guid variationId);
        Task<ApiResponse<VariationOptions>> AddVariationOptionAsync(VariationOptionsDto variationOptionsDto);
        Task<ApiResponse<VariationOptions>> UpdateVariationOptionAsync(VariationOptionsDto variationOptionsDto);
        Task<ApiResponse<VariationOptions>> GetVariationOptionByVariationIdAsync(Guid variationOptionId);
        Task<ApiResponse<VariationOptions>> DeleteVariationOptionByVariationIdAsync(Guid variationOptionId);
    }
}
