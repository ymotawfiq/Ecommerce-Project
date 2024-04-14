

using Ecommerce.Data;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Data.Models.Entities.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository.Repositories.UserPaymentMethodRepository
{
    public class UserPaymentMethodRepository : IUserPaymentMethod
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<SiteUser> _userManager;
        public UserPaymentMethodRepository(ApplicationDbContext _dbContext, UserManager<SiteUser> _userManager)
        {
            this._dbContext = _dbContext;
            this._userManager = _userManager;
        }

        public async Task<UserPaymentMethod> AddUserPaymentMethodAsync(UserPaymentMethod userPaymentMethod)
        {
            try
            {
                await _dbContext.UserPaymentMethod.AddAsync(userPaymentMethod);
                await SaveChangesAsync();
                return userPaymentMethod;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UserPaymentMethod> DeleteUserPaymentMethodByIdAsync(Guid userPaymentMethodId)
        {
            try
            {
                UserPaymentMethod userPaymentMethod = await GetUserPaymentMethodByIdAsync(userPaymentMethodId);
                _dbContext.UserPaymentMethod.Remove(userPaymentMethod);
                await SaveChangesAsync();
                return userPaymentMethod;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<UserPaymentMethod>> GetAllUsersPaymentsMethodsAsync()
        {
            try
            {
                return await _dbContext.UserPaymentMethod.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<UserPaymentMethod>> GetAllUsersPaymentsMethodsByUserIdAsync(string userId)
        {
            try
            {
                return
                    from u in await GetAllUsersPaymentsMethodsAsync()
                    where u.UserId == userId
                    select u;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<UserPaymentMethod>> 
            GetAllUsersPaymentsMethodsByUserUsernameOrEmailAsync(string usernameOrEmail)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(usernameOrEmail);
                return
                    await GetAllUsersPaymentsMethodsByUserIdAsync(user.Id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UserPaymentMethod> GetUserPaymentMethodByIdAsync(Guid userPaymentMethodId)
        {
            try
            {
                UserPaymentMethod? userPaymentMethod = await _dbContext.UserPaymentMethod
                    .Where(e => e.Id == userPaymentMethodId).FirstOrDefaultAsync();
                return userPaymentMethod;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<UserPaymentMethod> UpdateUserPaymentMethodAsync(UserPaymentMethod userPaymentMethod)
        {
            try
            {
                UserPaymentMethod oldUserPaymentMethod = await
                        GetUserPaymentMethodByIdAsync(userPaymentMethod.Id);
                oldUserPaymentMethod.AccountNumber = userPaymentMethod.AccountNumber;
                oldUserPaymentMethod.ExpiryDate = userPaymentMethod.ExpiryDate;
                oldUserPaymentMethod.IsDefault = userPaymentMethod.IsDefault;
                oldUserPaymentMethod.PaymentTypeId = userPaymentMethod.PaymentTypeId;
                oldUserPaymentMethod.Provider = userPaymentMethod.Provider;
                oldUserPaymentMethod.UserId = userPaymentMethod.UserId;
                await SaveChangesAsync();
                return oldUserPaymentMethod;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UserPaymentMethod> UpsertAsync(UserPaymentMethod userPaymentMethod)
        {
            try
            {
                UserPaymentMethod userPaymentMethod1 = await
                    GetUserPaymentMethodByIdAsync(userPaymentMethod.Id);
                if (userPaymentMethod == null)
                {
                    return await AddUserPaymentMethodAsync(userPaymentMethod1);
                }
                return await UpdateUserPaymentMethodAsync(userPaymentMethod1);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
