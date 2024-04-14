

using Ecommerce.Data.DTOs;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;

namespace Ecommerce.Service.Services.OrderLineService
{
    public interface IOrderLineService
    {
        Task<ApiResponse<OrderLine>> AddOrderLineAsync(OrderLineDto orderLineDto);
        Task<ApiResponse<OrderLine>> UpdateOrderLineAsync(OrderLineDto orderLineDto);
        Task<ApiResponse<OrderLine>> DeleteOrderLineByIdAsync(Guid orderLineId);
        Task<ApiResponse<OrderLine>> GetOrderLineByIdAsync(Guid orderLineId);
        Task<ApiResponse<IEnumerable<OrderLine>>> GetAllOrderLinesAsync();
        Task<ApiResponse<IEnumerable<OrderLine>>> GetAllOrderLinesByProductItemIdAsync(Guid productItemId);
        Task<ApiResponse<IEnumerable<OrderLine>>> GetAllOrderLinesByShopOrderIdAsync(Guid shopOrderId);
    }
}
