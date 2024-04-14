
using Ecommerce.Data.DTOs;
using Ecommerce.Data.Extensions;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Repository.Repositories.OrderLineRepository;
using Ecommerce.Repository.Repositories.ProductItemRepository;
using Ecommerce.Repository.Repositories.ShopOrderRepository;

namespace Ecommerce.Service.Services.OrderLineService
{
    public class OrderLineService : IOrderLineService
    {
        private readonly IOrderLine _orderLineRepository;
        private readonly IProductItem _productItemRepository;
        private readonly IShopOrder _shopOrderRepository;
        public OrderLineService(IOrderLine _orderLineRepository, IProductItem _productItemRepository,
            IShopOrder _shopOrderRepository)
        {
            this._orderLineRepository = _orderLineRepository;
            this._productItemRepository = _productItemRepository;
            this._shopOrderRepository = _shopOrderRepository;
        }

        public async Task<ApiResponse<OrderLine>> AddOrderLineAsync(OrderLineDto orderLineDto)
        {
            if (orderLineDto == null)
            {
                return new ApiResponse<OrderLine>
                {
                    IsSuccess = false,
                    Message = "Input must not be null",
                    StatusCode = 400
                };
            }
            ProductItem productItem = await _productItemRepository.GetProductItemByIdAsync(orderLineDto.ProductItemId);
            if (productItem == null)
            {
                return new ApiResponse<OrderLine>
                {
                    IsSuccess = false,
                    Message = "Product item not found",
                    StatusCode = 400
                };
            }
            ShopOrder shopOrder = await _shopOrderRepository.GetShopOrderByIdAsync(orderLineDto.ShopOrderId);
            if (shopOrder == null)
            {
                return new ApiResponse<OrderLine>
                {
                    IsSuccess = false,
                    Message = "Shop order not found",
                    StatusCode = 400
                };
            }
            OrderLine newOrderLine = await _orderLineRepository.AddOrderLineAsync(
                ConvertFromDto.ConvertFromOrderLineDto_Add(orderLineDto));
            return new ApiResponse<OrderLine>
            {
                IsSuccess = true,
                Message = "Shop order saved found",
                StatusCode = 201,
                ResponseObject = newOrderLine
            };
        }

        public async Task<ApiResponse<OrderLine>> DeleteOrderLineByIdAsync(Guid orderLineId)
        {
            OrderLine orderLine = await _orderLineRepository.GetOrderLineByIdAsync(orderLineId);
            if (orderLine == null)
            {
                return new ApiResponse<OrderLine>
                {
                    IsSuccess = false,
                    Message = "Order line not found",
                    StatusCode = 400
                };
            }
            OrderLine deletedOrderLine = await _orderLineRepository.DeleteOrderLineByIdAsync(orderLineId);
            return new ApiResponse<OrderLine>
            {
                IsSuccess = true,
                Message = "Order line deleted successfully",
                StatusCode = 200,
                ResponseObject = deletedOrderLine
            };
        }

        public async Task<ApiResponse<IEnumerable<OrderLine>>> GetAllOrderLinesAsync()
        {
            var orderLines = await _orderLineRepository.GetAllOrderLinesAsync();
            if (orderLines.ToList().Count == 0)
            {
                return new ApiResponse<IEnumerable<OrderLine>>
                {
                    IsSuccess = true,
                    Message = "No order lines found",
                    StatusCode = 200
                };
            }
            return new ApiResponse<IEnumerable<OrderLine>>
            {
                IsSuccess = true,
                Message = "Order lines found successfully",
                StatusCode = 200,
                ResponseObject = orderLines
            };
        }

        public async Task<ApiResponse<IEnumerable<OrderLine>>> GetAllOrderLinesByProductItemIdAsync(Guid productItemId)
        {
            var orderLines = await _orderLineRepository.GetAllOrderLinesByProductItemIdAsync(productItemId);
            if (orderLines.ToList().Count == 0)
            {
                return new ApiResponse<IEnumerable<OrderLine>>
                {
                    IsSuccess = true,
                    Message = "No order lines found",
                    StatusCode = 200
                };
            }
            return new ApiResponse<IEnumerable<OrderLine>>
            {
                IsSuccess = true,
                Message = "Order lines found successfully",
                StatusCode = 200,
                ResponseObject = orderLines
            };
        }

        public async Task<ApiResponse<IEnumerable<OrderLine>>> GetAllOrderLinesByShopOrderIdAsync(Guid shopOrderId)
        {
            var orderLines = await _orderLineRepository.GetAllOrderLinesByShopOrderIdAsync(shopOrderId);
            if (orderLines.ToList().Count == 0)
            {
                return new ApiResponse<IEnumerable<OrderLine>>
                {
                    IsSuccess = true,
                    Message = "No order lines found",
                    StatusCode = 200
                };
            }
            return new ApiResponse<IEnumerable<OrderLine>>
            {
                IsSuccess = true,
                Message = "Order lines found successfully",
                StatusCode = 200,
                ResponseObject = orderLines
            };
        }

        public async Task<ApiResponse<OrderLine>> GetOrderLineByIdAsync(Guid orderLineId)
        {
            OrderLine orderLine = await _orderLineRepository.GetOrderLineByIdAsync(orderLineId);
            if (orderLine == null)
            {
                return new ApiResponse<OrderLine>
                {
                    IsSuccess = false,
                    Message = "Order line not found",
                    StatusCode = 400
                };
            }
            return new ApiResponse<OrderLine>
            {
                IsSuccess = true,
                Message = "Order line found successfully",
                StatusCode = 200,
                ResponseObject = orderLine
            };
        }

        public async Task<ApiResponse<OrderLine>> UpdateOrderLineAsync(OrderLineDto orderLineDto)
        {
            if (orderLineDto == null)
            {
                return new ApiResponse<OrderLine>
                {
                    IsSuccess = false,
                    Message = "Input must not be null",
                    StatusCode = 400
                };
            }
            if (orderLineDto.Id == null)
            {
                return new ApiResponse<OrderLine>
                {
                    IsSuccess = false,
                    Message = "Order line id must not be null",
                    StatusCode = 400
                };
            }
            ProductItem productItem = await _productItemRepository.GetProductItemByIdAsync(orderLineDto.ProductItemId);
            if (productItem == null)
            {
                return new ApiResponse<OrderLine>
                {
                    IsSuccess = false,
                    Message = "Product item not found",
                    StatusCode = 400
                };
            }
            ShopOrder shopOrder = await _shopOrderRepository.GetShopOrderByIdAsync(orderLineDto.ShopOrderId);
            if (shopOrder == null)
            {
                return new ApiResponse<OrderLine>
                {
                    IsSuccess = false,
                    Message = "Shop order not found",
                    StatusCode = 400
                };
            }
            OrderLine oldOrderLine = await _orderLineRepository.GetOrderLineByIdAsync(new Guid(orderLineDto.Id));
            if (oldOrderLine == null)
            {
                return new ApiResponse<OrderLine>
                {
                    IsSuccess = false,
                    Message = "Order line not found",
                    StatusCode = 400
                };
            }
            OrderLine updatedOrderLine = await _orderLineRepository.UpdateOrderLineAsync(
                ConvertFromDto.ConvertFromOrderLineDto_Update(orderLineDto));
            return new ApiResponse<OrderLine>
            {
                IsSuccess = true,
                Message = "Shop order updated found",
                StatusCode = 200,
                ResponseObject = updatedOrderLine
            };
        }
    }
}
