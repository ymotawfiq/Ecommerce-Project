
using Ecommerce.Data.DTOs;
using Ecommerce.Data.Extensions;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Repository.Repositories.PaymentTypeRepository;

namespace Ecommerce.Service.Services.PaymentTypeService
{
    public class PaymentTypeService : IPaymentTypeService
    {
        private readonly IPaymentType _paymentTypeRepository;
        public PaymentTypeService(IPaymentType _paymentTypeRepository)
        {
            this._paymentTypeRepository = _paymentTypeRepository;
        }

        public async Task<ApiResponse<PaymentType>> AddPaymentTypeAsync(PaymentTypeDto paymentTypeDto)
        {
            if(paymentTypeDto == null)
            {
                return new ApiResponse<PaymentType>
                {
                    IsSuccess = false,
                    Message = "Input must not be null",
                    StatusCode = 400
                };
            }
            var newPaymentType = await _paymentTypeRepository.AddPaymentTypeAsync(
                ConvertFromDto.ConvertFromPaymentTypeDto_Add(paymentTypeDto));
            return new ApiResponse<PaymentType>
            {
                IsSuccess = true,
                Message = "Payment type added successfully",
                StatusCode = 201,
                ResponseObject = newPaymentType
            };
        }

        public async Task<ApiResponse<PaymentType>> DeletePaymentTypeByIdAsync(Guid paymentTypeId)
        {
            var paymentType = await _paymentTypeRepository.GetPaymentTypeByIdAsync(paymentTypeId);
            if(paymentType == null)
            {
                return new ApiResponse<PaymentType>
                {
                    IsSuccess = false,
                    Message = "No payment type found",
                    StatusCode = 400
                };
            }
            var deletedPaymentType = await _paymentTypeRepository.DeletePaymentTypeByIdAsync(paymentTypeId);
            return new ApiResponse<PaymentType>
            {
                IsSuccess = true,
                Message = "Payment type deleted successfully",
                StatusCode = 200,
                ResponseObject = deletedPaymentType
            };
        }

        public async Task<ApiResponse<IEnumerable<PaymentType>>> GetAllPaymentTypes()
        {
            var paymentTypes = await _paymentTypeRepository.GetAllPaymentTypesAsync();
            if(paymentTypes.ToList().Count == 0)
            {
                return new ApiResponse<IEnumerable<PaymentType>>
                {
                    IsSuccess = true,
                    Message = "No payment types found",
                    StatusCode = 200,
                    ResponseObject = paymentTypes
                };
            }
            return new ApiResponse<IEnumerable<PaymentType>>
            {
                IsSuccess = true,
                Message = "Payment types found successfully",
                StatusCode = 200,
                ResponseObject = paymentTypes
            };
        }

        public async Task<ApiResponse<PaymentType>> GetPaymentTypeByIdAsync(Guid paymentTypeId)
        {
            var paymentType = await _paymentTypeRepository.GetPaymentTypeByIdAsync(paymentTypeId);
            if (paymentType == null)
            {
                return new ApiResponse<PaymentType>
                {
                    IsSuccess = false,
                    Message = "No payment type found",
                    StatusCode = 400,
                };
            }
            return new ApiResponse<PaymentType>
            {
                IsSuccess = true,
                Message = "Payment type found successfully",
                StatusCode = 200,
                ResponseObject = paymentType
            };
        }

        public async Task<ApiResponse<PaymentType>> UpdatePaymentTypeAsync(PaymentTypeDto paymentTypeDto)
        {
            if (paymentTypeDto == null)
            {
                return new ApiResponse<PaymentType>
                {
                    IsSuccess = false,
                    Message = "Input must not be null",
                    StatusCode = 400
                };
            }
            if(paymentTypeDto.Id == null)
            {
                return new ApiResponse<PaymentType>
                {
                    IsSuccess = false,
                    Message = "Payment type id must not be null",
                    StatusCode = 400
                };
            }
            var oldPaymetType = await _paymentTypeRepository.GetPaymentTypeByIdAsync
                (new Guid(paymentTypeDto.Id));
            if (oldPaymetType == null)
            {
                return new ApiResponse<PaymentType>
                {
                    IsSuccess = false,
                    Message = "No payment type found",
                    StatusCode = 400
                };
            }
            var updatedPaymentType = await _paymentTypeRepository.UpdatePaymentTypeAsync(
                ConvertFromDto.ConvertFromPaymentTypeDto_Update(paymentTypeDto));
            return new ApiResponse<PaymentType>
            {
                IsSuccess = true,
                Message = "Payment type updated successfully",
                StatusCode = 200,
                ResponseObject = updatedPaymentType
            };
        }




    }
}
