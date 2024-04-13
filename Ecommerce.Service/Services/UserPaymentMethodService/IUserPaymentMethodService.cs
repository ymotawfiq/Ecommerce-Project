
using Ecommerce.Data.DTOs;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;

namespace Ecommerce.Service.Services.UserPaymentMethodService
{
    public interface IUserPaymentMethodService
    {
        public Task<ApiResponse<IEnumerable<UserPaymentMethod>>> GetAllUsersPaymentMethodsAsync();
        public Task<ApiResponse<IEnumerable<UserPaymentMethod>>> GetAllUsersPaymentMethodsAsyncByUserIdAsync
            (string userId);
        public Task<ApiResponse<IEnumerable<UserPaymentMethod>>> GetAllUsersPaymentMethodsAsyncByUserUsernameOrEmailAsync
            (string usernameOrEmail);
        public Task<ApiResponse<UserPaymentMethod>> AddUserPaymentMethodAsync(UserPaymentMethodDto userPaymentMethodDto);
        public Task<ApiResponse<UserPaymentMethod>> UpdateUserPaymentMethodAsync
            (UserPaymentMethodDto userPaymentMethodDto);
        public Task<ApiResponse<UserPaymentMethod>> DeleteUserPaymentMethodByIdAsync
            (Guid userPaymentMethodId);
        public Task<ApiResponse<UserPaymentMethod>> GetUserPaymentMethodByIdAsync
                    (Guid userPaymentMethodId);
    }
}
