

using Ecommerce.Data.DTOs;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;

namespace Ecommerce.Service.Services.OrderStatusService
{
    public interface IOrderStatusService
    {
        Task<ApiResponse<OrderStatus>> AddOrderStatusAsync(OrderStatusDto orderStatusDto);
        Task<ApiResponse<OrderStatus>> UpdateOrderStatusAsync(OrderStatusDto orderStatusDto);
        Task<ApiResponse<OrderStatus>> DeleteOrderStatusByIdAsync(Guid orderStatusId);
        Task<ApiResponse<OrderStatus>> GetOrderStatusByIdAsync(Guid orderStatusId);
        Task<ApiResponse<IEnumerable<OrderStatus>>> GetAllOrderStatusAsync();
    }
}
