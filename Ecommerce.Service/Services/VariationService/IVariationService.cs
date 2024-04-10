

using Ecommerce.Data.DTOs;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;

namespace Ecommerce.Service.Services.VariationService
{
    public interface IVariationService
    {
        Task<ApiResponse<IEnumerable<Variation>>> GetAllVariationsAsync();
        Task<ApiResponse<IEnumerable<Variation>>> GetAllVariationsByCategoryIdAsync(Guid categoryId);
        Task<ApiResponse<Variation>> AddVariationAsync(VariationDto variationDto);
        Task<ApiResponse<Variation>> UpdateVariationAsync(VariationDto variationDto);
        Task<ApiResponse<Variation>> DeleteVariationByIdAsync(Guid variationId);
        Task<ApiResponse<Variation>> GetVariationByIdAsync(Guid variationId);
    }
}
