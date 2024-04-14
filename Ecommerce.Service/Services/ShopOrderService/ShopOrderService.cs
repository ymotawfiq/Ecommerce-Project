

using Ecommerce.Data.DTOs;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Data.Models.Entities.Authentication;
using Ecommerce.Repository.Repositories.AddressRepository;
using Ecommerce.Repository.Repositories.OrderStatusRepository;
using Ecommerce.Repository.Repositories.ShippingMethodRepository;
using Ecommerce.Repository.Repositories.ShopOrderRepository;
using Ecommerce.Repository.Repositories.UserPaymentMethodRepository;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Service.Services.ShopOrderService
{
    public class ShopOrderService : IShopOrderService
    {
        private readonly IShopOrder _shopOrderRepository;
        private readonly UserManager<SiteUser> _userManager;
        private readonly IAddress _addressRepository;
        private readonly IUserPaymentMethod _paymentMethodRepository;
        private readonly IOrderStatus _orderStatusRepository;
        private readonly IShippingMethod _shippingMethodRepository;
        public ShopOrderService
            (
            IShopOrder _shopOrderRepository,
            UserManager<SiteUser> _userManager,
            IAddress _addressRepository,
            IUserPaymentMethod _paymentMethodRepository,
            IOrderStatus _orderStatusRepository,
            IShippingMethod _shippingMethodRepository
            )
        {
            this._shopOrderRepository = _shopOrderRepository;
            this._userManager = _userManager;
            this._addressRepository = _addressRepository;
            this._paymentMethodRepository = _paymentMethodRepository;
            this._orderStatusRepository = _orderStatusRepository;
            this._shippingMethodRepository = _shippingMethodRepository;
        }

        public async Task<ApiResponse<ShopOrder>> AddShopOrderAsync(ShopOrderDto shopOrderDto)
        {
            if (shopOrderDto == null)
            {
                return new ApiResponse<ShopOrder>
                {
                    IsSuccess = false,
                    Message = "Input must not be null",
                    StatusCode = 400
                };
            }

            OrderStatus orderStatus = await _orderStatusRepository
                .GetOrderStatusByIdAsync(shopOrderDto.OrderStatusId);
            if (orderStatus == null)
            {
                return new ApiResponse<ShopOrder>
                {
                    IsSuccess = false,
                    Message = "Order status not found",
                    StatusCode = 400
                };
            }

            Address address = await _addressRepository
                .GetAddressByIdAsync(shopOrderDto.ShippingAddressId);
            if (address == null)
            {
                return new ApiResponse<ShopOrder>
                {
                    IsSuccess = false,
                    Message = "Address not found for this user",
                    StatusCode = 400
                };
            }

            UserPaymentMethod userPaymentMethod = await _paymentMethodRepository
                .GetUserPaymentMethodByIdAsync(shopOrderDto.PaymentMethodId);
            if (userPaymentMethod == null)
            {
                return new ApiResponse<ShopOrder>
                {
                    IsSuccess = false,
                    Message = "User payment method not available for this user",
                    StatusCode = 400
                };
            }

            ShippingMethod shippingMethod = await _shippingMethodRepository
                .GetShippingMethodByIdAsync(shopOrderDto.ShippingMethodId);
            if (shippingMethod == null)
            {
                return new ApiResponse<ShopOrder>
                {
                    IsSuccess = false,
                    Message = "Shipping method not available for this user",
                    StatusCode = 400
                };
            }

            var user = await _userManager.FindByEmailAsync(shopOrderDto.UsernameOrEmail);
            if (user == null)
            {
                return new ApiResponse<ShopOrder>
                {
                    IsSuccess = false,
                    Message = "User not found",
                    StatusCode = 400
                };
            }

            ShopOrder shopOrder = new ShopOrder
            {
                OrderDate = shopOrderDto.OrderDate,
                OrderStatusId = shopOrderDto.OrderStatusId,
                PaymentMethodId = shopOrderDto.PaymentMethodId,
                ShippingAddressId = shopOrderDto.ShippingAddressId,
                ShippingMethodId = shopOrderDto.ShippingMethodId,
                OrderTotal = shopOrderDto.OrderTotal,
                UserId = user.Id
            };
            ShopOrder newShopOrder = await _shopOrderRepository.AddShopOrderAsync(shopOrder);
            return new ApiResponse<ShopOrder>
            {
                IsSuccess = true,
                Message = "Shop order added successfully",
                StatusCode = 201,
                ResponseObject = newShopOrder
            };
        }

        public async Task<ApiResponse<ShopOrder>> DeleteShopOrderByIdAsync(Guid shopOrderId)
        {
            ShopOrder shopOrder = await _shopOrderRepository.GetShopOrderByIdAsync(shopOrderId);
            if (shopOrder == null)
            {
                return new ApiResponse<ShopOrder>
                {
                    IsSuccess = true,
                    Message = "Shopping order not found",
                    StatusCode = 200
                };
            }
            ShopOrder deletedShopOrder = await _shopOrderRepository.DeleteShopOrderByIdAsync(shopOrderId);
            return new ApiResponse<ShopOrder>
            {
                IsSuccess = true,
                Message = "Shop order deleted successfully",
                StatusCode = 200
            };
        }

        public async Task<ApiResponse<IEnumerable<ShopOrder>>> GetAllShopOrdersAsync()
        {

            var shoppingOrders = await _shopOrderRepository
                .GetAllShopOrdersAsync();
            if (shoppingOrders.ToList().Count == 0)
            {
                return new ApiResponse<IEnumerable<ShopOrder>>
                {
                    IsSuccess = true,
                    Message = "No shopping orders found",
                    StatusCode = 200,
                    ResponseObject = shoppingOrders
                };
            }
            return new ApiResponse<IEnumerable<ShopOrder>>
            {
                IsSuccess = true,
                Message = "Shopping orders found successfully",
                StatusCode = 200,
                ResponseObject = shoppingOrders
            };
        }

        public async Task<ApiResponse<IEnumerable<ShopOrder>>> GetAllShopOrdersByAddressIdAsync(Guid shippingAddressId)
        {
            Address address = await _addressRepository
                            .GetAddressByIdAsync(shippingAddressId);
            if (address == null)
            {
                return new ApiResponse<IEnumerable<ShopOrder>>
                {
                    IsSuccess = false,
                    Message = "Address not found",
                    StatusCode = 400
                };
            }
            var shoppingOrders = await _shopOrderRepository
                .GetAllShopOrdersByAddressIdAsync(shippingAddressId);
            if (shoppingOrders.ToList().Count == 0)
            {
                return new ApiResponse<IEnumerable<ShopOrder>>
                {
                    IsSuccess = true,
                    Message = "No shopping orders found",
                    StatusCode = 200,
                    ResponseObject = shoppingOrders
                };
            }
            return new ApiResponse<IEnumerable<ShopOrder>>
            {
                IsSuccess = true,
                Message = "Shopping orders found successfully",
                StatusCode = 200,
                ResponseObject = shoppingOrders
            };
        }

        public async Task<ApiResponse<IEnumerable<ShopOrder>>> GetAllShopOrdersByDateAsync(DateTime orderDate)
        {
            var shoppingOrders = await _shopOrderRepository
                .GetAllShopOrdersByDateAsync(orderDate);
            if (shoppingOrders.ToList().Count == 0)
            {
                return new ApiResponse<IEnumerable<ShopOrder>>
                {
                    IsSuccess = true,
                    Message = "No shopping orders found",
                    StatusCode = 200,
                    ResponseObject = shoppingOrders
                };
            }
            return new ApiResponse<IEnumerable<ShopOrder>>
            {
                IsSuccess = true,
                Message = "Shopping orders found successfully",
                StatusCode = 200,
                ResponseObject = shoppingOrders
            };
        }

        public async Task<ApiResponse<IEnumerable<ShopOrder>>> GetAllShopOrdersByOrderPriceAsync(decimal orderTotlaPrice)
        {
            var shoppingOrders = await _shopOrderRepository
                .GetAllShopOrdersByOrderPriceAsync(orderTotlaPrice);
            if (shoppingOrders.ToList().Count == 0)
            {
                return new ApiResponse<IEnumerable<ShopOrder>>
                {
                    IsSuccess = true,
                    Message = "No shopping orders found",
                    StatusCode = 200,
                    ResponseObject = shoppingOrders
                };
            }
            return new ApiResponse<IEnumerable<ShopOrder>>
            {
                IsSuccess = true,
                Message = "Shopping orders found successfully",
                StatusCode = 200,
                ResponseObject = shoppingOrders
            };
        }

        public async Task<ApiResponse<IEnumerable<ShopOrder>>> GetAllShopOrdersByPaymentMethodIdAsync(Guid paymentMethodId)
        {
            UserPaymentMethod paymentMethod = await _paymentMethodRepository
                            .GetUserPaymentMethodByIdAsync(paymentMethodId);
            if (paymentMethod == null)
            {
                return new ApiResponse<IEnumerable<ShopOrder>>
                {
                    IsSuccess = false,
                    Message = "Payment method not found",
                    StatusCode = 400
                };
            }
            var shoppingOrders = await _shopOrderRepository
                .GetAllShopOrdersByPaymentMethodIdAsync(paymentMethodId);
            if (shoppingOrders.ToList().Count == 0)
            {
                return new ApiResponse<IEnumerable<ShopOrder>>
                {
                    IsSuccess = true,
                    Message = "No shopping orders found",
                    StatusCode = 200,
                    ResponseObject = shoppingOrders
                };
            }
            return new ApiResponse<IEnumerable<ShopOrder>>
            {
                IsSuccess = true,
                Message = "Shopping orders found successfully",
                StatusCode = 200,
                ResponseObject = shoppingOrders
            };
        }

        public async Task<ApiResponse<IEnumerable<ShopOrder>>> GetAllShopOrdersByShippingMethodIdAsync(Guid shippingMethodId)
        {
            ShippingMethod shippingMethod = await _shippingMethodRepository
                .GetShippingMethodByIdAsync(shippingMethodId);
            if (shippingMethod == null)
            {
                return new ApiResponse<IEnumerable<ShopOrder>>
                {
                    IsSuccess = false,
                    Message = "Shipping method not found",
                    StatusCode = 400
                };
            }
            var shoppingOrders = await _shopOrderRepository
                .GetAllShopOrdersByShippingMethodIdAsync(shippingMethodId);
            if (shoppingOrders.ToList().Count == 0)
            {
                return new ApiResponse<IEnumerable<ShopOrder>>
                {
                    IsSuccess = true,
                    Message = "No shopping orders found",
                    StatusCode = 200,
                    ResponseObject = shoppingOrders
                };
            }
            return new ApiResponse<IEnumerable<ShopOrder>>
            {
                IsSuccess = true,
                Message = "Shopping orders found successfully",
                StatusCode = 200,
                ResponseObject = shoppingOrders
            };
        }

        public async Task<ApiResponse<IEnumerable<ShopOrder>>> GetAllShopOrdersByUserUsernameOrEmailAsync(string usernameOrEmail)
        {
            var user = await _userManager.FindByEmailAsync(usernameOrEmail);
            if (user == null)
            {
                return new ApiResponse<IEnumerable<ShopOrder>>
                {
                    IsSuccess = false,
                    Message = "User not found",
                    StatusCode = 400
                };
            }
            var shoppingOrders = await _shopOrderRepository
                .GetAllShopOrdersByUserUsernameOrEmailAsync(usernameOrEmail);
            if (shoppingOrders.ToList().Count == 0)
            {
                return new ApiResponse<IEnumerable<ShopOrder>>
                {
                    IsSuccess = true,
                    Message = "No shopping orders found for this user",
                    StatusCode = 200,
                    ResponseObject = shoppingOrders
                };
            }
            return new ApiResponse<IEnumerable<ShopOrder>>
            {
                IsSuccess = true,
                Message = "Shopping orders found successfully for this user",
                StatusCode = 200,
                ResponseObject = shoppingOrders
            };
        }

        public async Task<ApiResponse<ShopOrder>> GetShopOrderByIdAsync(Guid shopOrderId)
        {
            ShopOrder shopOrder = await _shopOrderRepository.GetShopOrderByIdAsync(shopOrderId);
            if (shopOrder == null)
            {
                return new ApiResponse<ShopOrder>
                {
                    IsSuccess = false,
                    Message = "Shop order not found",
                    StatusCode = 400
                };
            }
            return new ApiResponse<ShopOrder>
            {
                IsSuccess = true,
                Message = "Shop order found successfully",
                StatusCode = 200,
                ResponseObject = shopOrder
            };
        }

        public async Task<ApiResponse<ShopOrder>> UpdateShopOrderAsync(ShopOrderDto shopOrderDto)
        {
            if (shopOrderDto == null)
            {
                return new ApiResponse<ShopOrder>
                {
                    IsSuccess = false,
                    Message = "Input must not be null",
                    StatusCode = 400
                };
            }

            if (shopOrderDto.Id == null)
            {
                return new ApiResponse<ShopOrder>
                {
                    IsSuccess = false,
                    Message = "Shop order id must not be null",
                    StatusCode = 400
                };
            }

            ShopOrder oldShopOrder = await _shopOrderRepository.GetShopOrderByIdAsync(new Guid(shopOrderDto.Id));
            if (oldShopOrder == null)
            {
                return new ApiResponse<ShopOrder>
                {
                    IsSuccess = false,
                    Message = "Shop order not found",
                    StatusCode = 400
                };
            }

            OrderStatus orderStatus = await _orderStatusRepository
                .GetOrderStatusByIdAsync(shopOrderDto.OrderStatusId);
            if (orderStatus == null)
            {
                return new ApiResponse<ShopOrder>
                {
                    IsSuccess = false,
                    Message = "Order status not found",
                    StatusCode = 400
                };
            }

            Address address = await _addressRepository
                .GetAddressByIdAsync(shopOrderDto.ShippingAddressId);
            if (address == null)
            {
                return new ApiResponse<ShopOrder>
                {
                    IsSuccess = false,
                    Message = "Address not found for this user",
                    StatusCode = 400
                };
            }

            UserPaymentMethod userPaymentMethod = await _paymentMethodRepository
                .GetUserPaymentMethodByIdAsync(shopOrderDto.PaymentMethodId);
            if (userPaymentMethod == null)
            {
                return new ApiResponse<ShopOrder>
                {
                    IsSuccess = false,
                    Message = "User payment method not available for this user",
                    StatusCode = 400
                };
            }

            ShippingMethod shippingMethod = await _shippingMethodRepository
                .GetShippingMethodByIdAsync(shopOrderDto.ShippingMethodId);
            if (shippingMethod == null)
            {
                return new ApiResponse<ShopOrder>
                {
                    IsSuccess = false,
                    Message = "Shipping method not available for this user",
                    StatusCode = 400
                };
            }

            var user = await _userManager.FindByEmailAsync(shopOrderDto.UsernameOrEmail);
            if (user == null)
            {
                return new ApiResponse<ShopOrder>
                {
                    IsSuccess = false,
                    Message = "User not found",
                    StatusCode = 400
                };
            }

            ShopOrder shopOrder = new ShopOrder
            {
                Id = new Guid(shopOrderDto.Id),
                OrderDate = shopOrderDto.OrderDate,
                OrderStatusId = shopOrderDto.OrderStatusId,
                PaymentMethodId = shopOrderDto.PaymentMethodId,
                ShippingAddressId = shopOrderDto.ShippingAddressId,
                ShippingMethodId = shopOrderDto.ShippingMethodId,
                OrderTotal = shopOrderDto.OrderTotal,
                UserId = user.Id
            };
            ShopOrder updatedShopOrder = await _shopOrderRepository.UpdateShopOrderAsync(shopOrder);
            return new ApiResponse<ShopOrder>
            {
                IsSuccess = true,
                Message = "Shop order updated successfully",
                StatusCode = 200,
                ResponseObject = updatedShopOrder
            };
        }
    }
}
