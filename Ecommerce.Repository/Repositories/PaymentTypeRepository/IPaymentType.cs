

using Ecommerce.Data.Models.Entities;

namespace Ecommerce.Repository.Repositories.PaymentTypeRepository
{
    public interface IPaymentType
    {
        public Task<PaymentType> AddPaymentTypeAsync(PaymentType paymentType);
        public Task<PaymentType> UpdatePaymentTypeAsync(PaymentType paymentType);
        public Task<PaymentType> DeletePaymentTypeByIdAsync(Guid paymentTypeId);
        public Task<PaymentType> GetPaymentTypeByIdAsync(Guid paymentTypeId);
        public Task<IEnumerable<PaymentType>> GetAllPaymentTypesAsync();
        public Task SaveChangesAsync();
        public Task<PaymentType> UpsertAsync(PaymentType paymentType);
    }
}
