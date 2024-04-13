

using Ecommerce.Data.DTOs;
using Ecommerce.Data.Extensions;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Repository.Repositories.ShippingMethodRepository;

namespace Ecommerce.Service.Services.ShippingMethodService
{
    public class ShippingMethodService : IShippingMethodService
    {
        private readonly IShippingMethod _shippingMethodRepository;
        public ShippingMethodService(IShippingMethod _shippingMethodRepository)
        {
            this._shippingMethodRepository = _shippingMethodRepository;
        }

        public async Task<ApiResponse<ShippingMethod>> AddShippingMethodAsync(ShippingMethodDto shippingMethodDto)
        {
            if (shippingMethodDto == null)
            {
                return new ApiResponse<ShippingMethod>
                {
                    IsSuccess = false,
                    Message = "Input must not be null",
                    StatusCode = 400
                };
            }
            ShippingMethod newShippingMethod = await _shippingMethodRepository
                .AddShippingMethodAsync(ConvertFromDto.ConvertFromShippingMethodDto_Add(shippingMethodDto));
            return new ApiResponse<ShippingMethod>
            {
                IsSuccess = true,
                Message = "Shipping method saved successfully",
                StatusCode = 201,
                ResponseObject = newShippingMethod
            };
        }

        public async Task<ApiResponse<ShippingMethod>> DeleteShippingMethodByIdAsync(Guid shippingMethodId)
        {
            ShippingMethod shippingMethod = await _shippingMethodRepository.GetShippingMethodByIdAsync(shippingMethodId);
            if (shippingMethod == null)
            {
                return new ApiResponse<ShippingMethod>
                {
                    IsSuccess = false,
                    Message = "Shipping method not found",
                    StatusCode = 400
                };
            }
            ShippingMethod deletedShippingMethod = await _shippingMethodRepository.DeleteShippingMethodByIdAsync(shippingMethodId);
            return new ApiResponse<ShippingMethod>
            {
                IsSuccess = true,
                Message = "Shipping method deleted successfully",
                StatusCode = 200,
                ResponseObject = deletedShippingMethod
            };
        }

        public async Task<ApiResponse<IEnumerable<ShippingMethod>>> GetAllShippingMethodAsync()
        {
            var shippingMethods = await _shippingMethodRepository.GetAllShippingMethodAsync();
            if (shippingMethods.ToList().Count == 0)
            {
                return new ApiResponse<IEnumerable<ShippingMethod>>
                {
                    IsSuccess = true,
                    Message = "No shipping methods found",
                    StatusCode = 200,
                    ResponseObject = shippingMethods
                };
            }
            return new ApiResponse<IEnumerable<ShippingMethod>>
            {
                IsSuccess = true,
                Message = "No shipping found successfully",
                StatusCode = 200,
                ResponseObject = shippingMethods
            };
        }

        public async Task<ApiResponse<ShippingMethod>> GetShippingMethodByIdAsync(Guid shippingMethodId)
        {
            ShippingMethod shippingMethod = await _shippingMethodRepository.GetShippingMethodByIdAsync(shippingMethodId);
            if (shippingMethod == null)
            {
                return new ApiResponse<ShippingMethod>
                {
                    IsSuccess = false,
                    Message = "No shipping methods found",
                    StatusCode = 400,
                    ResponseObject = shippingMethod
                };
            }
            return new ApiResponse<ShippingMethod>
            {
                IsSuccess = true,
                Message = "Shipping methods found successfully",
                StatusCode = 200,
                ResponseObject = shippingMethod
            };
        }

        public async Task<ApiResponse<ShippingMethod>> UpdateShippingMethodAsync
            (ShippingMethodDto shippingMethodDto)
        {
            if (shippingMethodDto == null)
            {
                return new ApiResponse<ShippingMethod>
                {
                    IsSuccess = false,
                    Message = "Input must not be null",
                    StatusCode = 400
                };
            }
            if (shippingMethodDto.Id == null)
            {
                return new ApiResponse<ShippingMethod>
                {
                    IsSuccess = false,
                    Message = "Shipping method id must not be null",
                    StatusCode = 400
                };
            }
            ShippingMethod newShippingMethod = await _shippingMethodRepository
                .UpdateShippingMethodAsync(ConvertFromDto.ConvertFromShippingMethodDto_Update(shippingMethodDto));
            return new ApiResponse<ShippingMethod>
            {
                IsSuccess = true,
                Message = "Shipping method updated successfully",
                StatusCode = 200,
                ResponseObject = newShippingMethod
            };
        }
    }
}
