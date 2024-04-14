

using Ecommerce.Data.DTOs;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;

namespace Ecommerce.Service.Services.ShopOrderService
{
    public interface IShopOrderService
    {
        Task<ApiResponse<ShopOrder>> AddShopOrderAsync(ShopOrderDto shopOrderDto);
        Task<ApiResponse<ShopOrder>> UpdateShopOrderAsync(ShopOrderDto shopOrderDto);
        Task<ApiResponse<ShopOrder>> DeleteShopOrderByIdAsync(Guid shopOrderId);
        Task<ApiResponse<ShopOrder>> GetShopOrderByIdAsync(Guid shopOrderId);
        Task<ApiResponse<IEnumerable<ShopOrder>>> GetAllShopOrdersAsync();
        Task<ApiResponse<IEnumerable<ShopOrder>>> GetAllShopOrdersByDateAsync(DateTime orderDate);
        Task<ApiResponse<IEnumerable<ShopOrder>>> GetAllShopOrdersByUserUsernameOrEmailAsync(string usernameOrEmail);
        Task<ApiResponse<IEnumerable<ShopOrder>>> GetAllShopOrdersByAddressIdAsync(Guid shippingAddressId);
        Task<ApiResponse<IEnumerable<ShopOrder>>> GetAllShopOrdersByPaymentMethodIdAsync(Guid paymentMethodId);
        Task<ApiResponse<IEnumerable<ShopOrder>>> GetAllShopOrdersByShippingMethodIdAsync(Guid shippingMethodId);
        Task<ApiResponse<IEnumerable<ShopOrder>>> GetAllShopOrdersByOrderPriceAsync(decimal orderTotlaPrice);
    }
}
