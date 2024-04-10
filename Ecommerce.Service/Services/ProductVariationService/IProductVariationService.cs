
using Ecommerce.Data.DTOs;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;

namespace Ecommerce.Service.Services.ProductVariationService
{
    public interface IProductVariationService
    {
        Task<ApiResponse<IEnumerable<ProductVariation>>> GetAllProductVariationsAsync();
        Task<ApiResponse<IEnumerable<ProductVariation>>> GetAllProductVariationsByVariationOptionIdAsync(Guid variationOptionId);
        Task<ApiResponse<IEnumerable<ProductVariation>>> GetAllProductVariationsByProductItemIdAsync(Guid productItemId);
        Task<ApiResponse<ProductVariation>> AddProductVariationAsync(ProductVariationDto productVariationDto);
        Task<ApiResponse<ProductVariation>> UpdateProductVariationAsync(ProductVariationDto productVariationDto);
        Task<ApiResponse<ProductVariation>> GetProductVariationByIdAsync(Guid productVariationId);
        Task<ApiResponse<ProductVariation>> DeleteProductVariationByIdAsync(Guid productVariationId);
    }
}
