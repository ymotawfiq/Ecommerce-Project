

using Ecommerce.Data.Models.Entities;

namespace Ecommerce.Repository.Repositories.UserPaymentMethodRepository
{
    public interface IUserPaymentMethod
    {
        public Task<UserPaymentMethod> AddUserPaymentMethodAsync(UserPaymentMethod userPaymentMethod);
        public Task<UserPaymentMethod> UpdateUserPaymentMethodAsync(UserPaymentMethod userPaymentMethod);
        public Task<UserPaymentMethod> DeleteUserPaymentMethodByIdAsync(Guid userPaymentMethodId);
        public Task<UserPaymentMethod> GetUserPaymentMethodByIdAsync(Guid userPaymentMethodId);
        public Task<IEnumerable<UserPaymentMethod>> GetAllUsersPaymentsMethodsAsync();
        public Task<IEnumerable<UserPaymentMethod>> GetAllUsersPaymentsMethodsByUserIdAsync(string userId);
        public Task<IEnumerable<UserPaymentMethod>> GetAllUsersPaymentsMethodsByUserUsernameOrEmailAsync
            (string usernameOrEmail);
        public Task SaveChangesAsync();
        public Task<UserPaymentMethod> UpsertAsync(UserPaymentMethod userPaymentMethod);
    }
}
