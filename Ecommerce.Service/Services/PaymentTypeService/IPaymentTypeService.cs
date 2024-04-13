

using Ecommerce.Data.DTOs;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;

namespace Ecommerce.Service.Services.PaymentTypeService
{
    public interface IPaymentTypeService
    {
        public Task<ApiResponse<PaymentType>> AddPaymentTypeAsync(PaymentTypeDto paymentTypeDto);
        public Task<ApiResponse<PaymentType>> UpdatePaymentTypeAsync(PaymentTypeDto paymentTypeDto);
        public Task<ApiResponse<PaymentType>> DeletePaymentTypeByIdAsync(Guid paymentTypeId);
        public Task<ApiResponse<PaymentType>> GetPaymentTypeByIdAsync(Guid paymentTypeId);
        public Task<ApiResponse<IEnumerable<PaymentType>>> GetAllPaymentTypes();
    }
}
