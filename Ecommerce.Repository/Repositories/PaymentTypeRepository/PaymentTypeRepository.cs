
using Ecommerce.Data;
using Ecommerce.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository.Repositories.PaymentTypeRepository
{
    public class PaymentTypeRepository : IPaymentType
    {
        private readonly ApplicationDbContext _dbContext;
        public PaymentTypeRepository(ApplicationDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        public async Task<PaymentType> AddPaymentTypeAsync(PaymentType paymentType)
        {
            try
            {
                await _dbContext.PaymentType.AddAsync(paymentType);
                await SaveChangesAsync();
                return paymentType;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PaymentType> DeletePaymentTypeByIdAsync(Guid paymentTypeId)
        {
            try
            {
                PaymentType paymentType = await GetPaymentTypeByIdAsync(paymentTypeId);
                _dbContext.PaymentType.Remove(paymentType);
                await SaveChangesAsync();
                return paymentType;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<PaymentType>> GetAllPaymentTypesAsync()
        {
            try
            {
                return await _dbContext.PaymentType.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PaymentType> GetPaymentTypeByIdAsync(Guid paymentTypeId)
        {
            try
            {
                PaymentType? paymentType = await _dbContext.PaymentType
                    .Where(e => e.Id == paymentTypeId).FirstOrDefaultAsync();
                return paymentType;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PaymentType> UpdatePaymentTypeAsync(PaymentType paymentType)
        {
            try
            {
                PaymentType oldPaymentType = await GetPaymentTypeByIdAsync(paymentType.Id);
                oldPaymentType.Value = paymentType.Value;
                await SaveChangesAsync();
                return oldPaymentType;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PaymentType> UpsertAsync(PaymentType paymentType)
        {
            try
            {
                PaymentType oldPaymentType = await GetPaymentTypeByIdAsync(paymentType.Id);
                if (oldPaymentType == null)
                {
                    return await AddPaymentTypeAsync(paymentType);
                }
                return await UpdatePaymentTypeAsync(paymentType);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
