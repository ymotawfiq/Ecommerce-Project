

using Ecommerce.Data.DTOs;
using Ecommerce.Data.Extensions;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Repository.Repositories.OrderStatusRepository;

namespace Ecommerce.Service.Services.OrderStatusService
{
    public class OrderStatusService : IOrderStatusService
    {
        private readonly IOrderStatus _orderStatusRepository;
        public OrderStatusService(IOrderStatus _orderStatusRepository)
        {
            this._orderStatusRepository = _orderStatusRepository;
        }

        public async Task<ApiResponse<OrderStatus>> AddOrderStatusAsync(OrderStatusDto orderStatusDto)
        {
            if (orderStatusDto == null)
            {
                return new ApiResponse<OrderStatus>
                {
                    IsSuccess = false,
                    Message = "Input must not be null",
                    StatusCode = 400
                };
            }
            OrderStatus newOrderStatus = await _orderStatusRepository.AddOrderStatusAsync(
                ConvertFromDto.ConvertFromOrderStatusDto_Add(orderStatusDto));
            return new ApiResponse<OrderStatus>
            {
                IsSuccess = true,
                Message = "Order status saved successfully",
                StatusCode = 201,
                ResponseObject = newOrderStatus
            };
        }

        public async Task<ApiResponse<OrderStatus>> DeleteOrderStatusByIdAsync(Guid orderStatusId)
        {
            OrderStatus orderStatus = await _orderStatusRepository.GetOrderStatusByIdAsync(orderStatusId);
            if (orderStatus == null)
            {
                return new ApiResponse<OrderStatus>
                {
                    IsSuccess = false,
                    Message = "Order status not be null",
                    StatusCode = 400
                };
            }
            OrderStatus deletedOrderStatus = await _orderStatusRepository.DeleteOrderStatusByIdAsync(orderStatusId);
            return new ApiResponse<OrderStatus>
            {
                IsSuccess = true,
                Message = "Order status deleted successfully",
                StatusCode = 200,
                ResponseObject = deletedOrderStatus
            };
        }

        public async Task<ApiResponse<IEnumerable<OrderStatus>>> GetAllOrderStatusAsync()
        {
            var orderStatus = await _orderStatusRepository.GetAllOrdersStatusAsync();
            if (orderStatus.ToList().Count == 0)
            {
                return new ApiResponse<IEnumerable<OrderStatus>>
                {
                    IsSuccess = true,
                    Message = "No order status found",
                    StatusCode = 200,
                    ResponseObject = orderStatus
                };
            }
            return new ApiResponse<IEnumerable<OrderStatus>>
            {
                IsSuccess = true,
                Message = "Order status found successfully",
                StatusCode = 200,
                ResponseObject = orderStatus
            };
        }

        public async Task<ApiResponse<OrderStatus>> GetOrderStatusByIdAsync(Guid orderStatusId)
        {
            OrderStatus orderStatus = await _orderStatusRepository.GetOrderStatusByIdAsync(orderStatusId);
            if (orderStatus == null)
            {
                return new ApiResponse<OrderStatus>
                {
                    IsSuccess = false,
                    Message = "Order status not be null",
                    StatusCode = 400
                };
            }
            return new ApiResponse<OrderStatus>
            {
                IsSuccess = true,
                Message = "Order status found successfully",
                StatusCode = 200,
                ResponseObject = orderStatus
            };
        }

        public async Task<ApiResponse<OrderStatus>> UpdateOrderStatusAsync(OrderStatusDto orderStatusDto)
        {
            if (orderStatusDto == null)
            {
                return new ApiResponse<OrderStatus>
                {
                    IsSuccess = false,
                    Message = "Input must not be null",
                    StatusCode = 400
                };
            }
            if (orderStatusDto.Id == null)
            {
                return new ApiResponse<OrderStatus>
                {
                    IsSuccess = false,
                    Message = "Order status id must not be null",
                    StatusCode = 400
                };
            }
            OrderStatus oldOrderStatus = await _orderStatusRepository
                .GetOrderStatusByIdAsync(new Guid(orderStatusDto.Id));
            if (orderStatusDto == null)
            {
                return new ApiResponse<OrderStatus>
                {
                    IsSuccess = false,
                    Message = "Order status inot found",
                    StatusCode = 400,
                    ResponseObject = oldOrderStatus
                };
            }
            OrderStatus updatedOrderStatus = await _orderStatusRepository.UpdteOrderStatusAsync(
                ConvertFromDto.ConvertFromOrderStatusDto_Update(orderStatusDto));
            return new ApiResponse<OrderStatus>
            {
                IsSuccess = true,
                Message = "Order status updated successfully",
                StatusCode = 200,
                ResponseObject = updatedOrderStatus
            };
        }
    }
}
