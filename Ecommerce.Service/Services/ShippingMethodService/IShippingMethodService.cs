

using Ecommerce.Data.DTOs;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;

namespace Ecommerce.Service.Services.ShippingMethodService
{
    public interface IShippingMethodService
    {
        Task<ApiResponse<ShippingMethod>> AddShippingMethodAsync(ShippingMethodDto shippingMethodDto);
        Task<ApiResponse<ShippingMethod>> UpdateShippingMethodAsync(ShippingMethodDto shippingMethodDto);
        Task<ApiResponse<ShippingMethod>> DeleteShippingMethodByIdAsync(Guid shippingMethodId);
        Task<ApiResponse<ShippingMethod>> GetShippingMethodByIdAsync(Guid shippingMethodId);
        Task<ApiResponse<IEnumerable<ShippingMethod>>> GetAllShippingMethodAsync();
    }
}
