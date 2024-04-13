

using Ecommerce.Data.DTOs;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Data.Models.Entities.Authentication;
using Ecommerce.Repository.Repositories.PaymentTypeRepository;
using Ecommerce.Repository.Repositories.UserPaymentMethodRepository;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Service.Services.UserPaymentMethodService
{
    public class UserPaymentMethodService : IUserPaymentMethodService
    {
        private readonly IUserPaymentMethod _userPaymentMethodRepository;
        private readonly UserManager<SiteUser> _userManager;
        private readonly IPaymentType _paymentTypeRepository;
        public UserPaymentMethodService
            (
            IUserPaymentMethod _userPaymentMethodRepository,
            UserManager<SiteUser> _userManager,
            IPaymentType _paymentTypeRepository
            )
        {
            this._paymentTypeRepository = _paymentTypeRepository;
            this._userManager = _userManager;
            this._userPaymentMethodRepository = _userPaymentMethodRepository;
        }

        public async Task<ApiResponse<UserPaymentMethod>> AddUserPaymentMethodAsync
            (UserPaymentMethodDto userPaymentMethodDto)
        {
            if(userPaymentMethodDto == null)
            {
                return new ApiResponse<UserPaymentMethod>
                {
                    IsSuccess = false,
                    Message = "Input must not be null",
                    StatusCode = 400
                };
            }
            PaymentType paymentType = await _paymentTypeRepository
                .GetPaymentTypeByIdAsync(new Guid(userPaymentMethodDto.PaymentTypeId));
            if (paymentType == null)
            {
                return new ApiResponse<UserPaymentMethod>
                {
                    IsSuccess = false,
                    Message = "Payment type not found",
                    StatusCode = 400
                };
            }
            try
            {
                var userId = new Guid(userPaymentMethodDto.UserIdOrEmail);
                var user = await _userManager.FindByIdAsync(userId.ToString());
                if(user == null)
                {
                    return new ApiResponse<UserPaymentMethod>
                    {
                        IsSuccess = false,
                        Message = "User not found",
                        StatusCode = 400
                    };
                }
                UserPaymentMethod userPaymentMethod = new UserPaymentMethod
                {
                    AccountNumber = userPaymentMethodDto.AccountNumber,
                    ExpiryDate = userPaymentMethodDto.ExpiryDate,
                    IsDefault = userPaymentMethodDto.IsDefault,
                    PaymentTypeId = new Guid(userPaymentMethodDto.PaymentTypeId),
                    Provider = userPaymentMethodDto.Provider,
                    UserId = userPaymentMethodDto.UserIdOrEmail
                };
                var newUserPaymentMethod = await _userPaymentMethodRepository
                    .AddUserPaymentMethodAsync(userPaymentMethod);
                return new ApiResponse<UserPaymentMethod>
                {
                    IsSuccess = true,
                    Message = "User payment method added successfully",
                    StatusCode = 201,
                    ResponseObject = newUserPaymentMethod
                };
            }
            catch (Exception)
            {
                var user = await _userManager.FindByEmailAsync(userPaymentMethodDto.UserIdOrEmail);
                if (user == null)
                {
                    return new ApiResponse<UserPaymentMethod>
                    {
                        IsSuccess = false,
                        Message = "User not found",
                        StatusCode = 400
                    };
                }
                UserPaymentMethod userPaymentMethod = new UserPaymentMethod
                {
                    AccountNumber = userPaymentMethodDto.AccountNumber,
                    ExpiryDate = userPaymentMethodDto.ExpiryDate,
                    IsDefault = userPaymentMethodDto.IsDefault,
                    PaymentTypeId = new Guid(userPaymentMethodDto.PaymentTypeId),
                    Provider = userPaymentMethodDto.Provider,
                    UserId = user.Id
                };
                var newUserPaymentMethod = await _userPaymentMethodRepository
                    .AddUserPaymentMethodAsync(userPaymentMethod);
                return new ApiResponse<UserPaymentMethod>
                {
                    IsSuccess = true,
                    Message = "User payment method added successfully",
                    StatusCode = 200,
                    ResponseObject = newUserPaymentMethod
                };
            }
        }

        public async Task<ApiResponse<UserPaymentMethod>> DeleteUserPaymentMethodByIdAsync(Guid userPaymentMethodId)
        {
            UserPaymentMethod userPaymentMethod = await _userPaymentMethodRepository
                .GetUserPaymentMethodByIdAsync(userPaymentMethodId);
            if (userPaymentMethod == null)
            {
                return new ApiResponse<UserPaymentMethod>
                {
                    IsSuccess = false,
                    Message = "User payment method not found",
                    StatusCode = 400
                };
            }
            return new ApiResponse<UserPaymentMethod>
            {
                IsSuccess = true,
                Message = "User payment method deleted successfully",
                StatusCode = 200,
                ResponseObject = userPaymentMethod
            };
        }

        public async Task<ApiResponse<IEnumerable<UserPaymentMethod>>> GetAllUsersPaymentMethodsAsync()
        {
            var userPaymentMethods = await _userPaymentMethodRepository.GetAllUsersPaymentsMethodsAsync();
            if (userPaymentMethods.ToList().Count == 0)
            {
                return new ApiResponse<IEnumerable<UserPaymentMethod>>
                {
                    IsSuccess = true,
                    Message = "No payment methods found",
                    StatusCode = 200,
                    ResponseObject = userPaymentMethods
                };
            }
            return new ApiResponse<IEnumerable<UserPaymentMethod>>
            {
                IsSuccess = true,
                Message = "Payment methods found successfully",
                StatusCode = 200,
                ResponseObject = userPaymentMethods
            };
        }

        public async Task<ApiResponse<IEnumerable<UserPaymentMethod>>> GetAllUsersPaymentMethodsAsyncByUserIdAsync
            (string userId)
        {
            var userPaymentMethods = await _userPaymentMethodRepository
                .GetAllUsersPaymentsMethodsByUserIdAsync(userId);
            if (userPaymentMethods.ToList().Count == 0)
            {
                return new ApiResponse<IEnumerable<UserPaymentMethod>>
                {
                    IsSuccess = true,
                    Message = "No payment methods found",
                    StatusCode = 200,
                    ResponseObject = userPaymentMethods
                };
            }
            return new ApiResponse<IEnumerable<UserPaymentMethod>>
            {
                IsSuccess = true,
                Message = "Payment methods found successfully",
                StatusCode = 200,
                ResponseObject = userPaymentMethods
            };
        }

        public async Task<ApiResponse<IEnumerable<UserPaymentMethod>>> 
            GetAllUsersPaymentMethodsAsyncByUserUsernameOrEmailAsync(string usernameOrEmail)
        {
            var user = await _userManager.FindByNameAsync(usernameOrEmail);
            if (user == null)
            {
                return new ApiResponse<IEnumerable<UserPaymentMethod>>
                {
                    IsSuccess = false,
                    Message = "No user payment methods found",
                    StatusCode = 400,
                };
            }
            var userPaymentMethods = await _userPaymentMethodRepository
                .GetAllUsersPaymentsMethodsByUserIdAsync(user.Id);

            if (userPaymentMethods.ToList().Count == 0)
            {
                return new ApiResponse<IEnumerable<UserPaymentMethod>>
                {
                    IsSuccess = true,
                    Message = "No payment methods found",
                    StatusCode = 200,
                    ResponseObject = userPaymentMethods
                };
            }
            return new ApiResponse<IEnumerable<UserPaymentMethod>>
            {
                IsSuccess = true,
                Message = "Payment methods found successfully",
                StatusCode = 200,
                ResponseObject = userPaymentMethods
            };
        }

        public async Task<ApiResponse<UserPaymentMethod>> GetUserPaymentMethodByIdAsync(Guid userPaymentMethodId)
        {
            UserPaymentMethod userPaymentMethod = await _userPaymentMethodRepository
                .GetUserPaymentMethodByIdAsync(userPaymentMethodId);
            if (userPaymentMethod == null)
            {
                return new ApiResponse<UserPaymentMethod>
                {
                    IsSuccess = false,
                    Message = "Payment method not found",
                    StatusCode = 400,
                    ResponseObject = userPaymentMethod
                };
            }
            return new ApiResponse<UserPaymentMethod>
            {
                IsSuccess = true,
                Message = "Payment method found successfully",
                StatusCode = 200,
                ResponseObject = userPaymentMethod
            };
        }

        public async Task<ApiResponse<UserPaymentMethod>> UpdateUserPaymentMethodAsync
            (UserPaymentMethodDto userPaymentMethodDto)
        {
            if (userPaymentMethodDto == null)
            {
                return new ApiResponse<UserPaymentMethod>
                {
                    IsSuccess = false,
                    Message = "Input must not be null",
                    StatusCode = 400
                };
            }
            PaymentType paymentType = await _paymentTypeRepository
                .GetPaymentTypeByIdAsync(new Guid(userPaymentMethodDto.PaymentTypeId));
            if (paymentType == null)
            {
                return new ApiResponse<UserPaymentMethod>
                {
                    IsSuccess = false,
                    Message = "Payment type not found",
                    StatusCode = 400
                };
            }
            if (userPaymentMethodDto.Id == null)
            {
                return new ApiResponse<UserPaymentMethod>
                {
                    IsSuccess = false,
                    Message = "Payment method id must not be null",
                    StatusCode = 400
                };
            }
            try
            {
                var userId = new Guid(userPaymentMethodDto.UserIdOrEmail);
                var user = await _userManager.FindByIdAsync(userId.ToString());
                if (user == null)
                {
                    return new ApiResponse<UserPaymentMethod>
                    {
                        IsSuccess = false,
                        Message = "User not found",
                        StatusCode = 400
                    };
                }
                UserPaymentMethod userPaymentMethod = new UserPaymentMethod
                {
                    Id = new Guid(userPaymentMethodDto.Id),
                    AccountNumber = userPaymentMethodDto.AccountNumber,
                    ExpiryDate = userPaymentMethodDto.ExpiryDate,
                    IsDefault = userPaymentMethodDto.IsDefault,
                    PaymentTypeId = new Guid(userPaymentMethodDto.PaymentTypeId),
                    Provider = userPaymentMethodDto.Provider,
                    UserId = userPaymentMethodDto.UserIdOrEmail
                };
                var updatedUserPaymentMethod = await _userPaymentMethodRepository
                    .UpdateUserPaymentMethodAsync(userPaymentMethod);
                return new ApiResponse<UserPaymentMethod>
                {
                    IsSuccess = true,
                    Message = "User payment method updated successfully",
                    StatusCode = 200,
                    ResponseObject = updatedUserPaymentMethod
                };
            }
            catch (Exception)
            {
                var user = await _userManager.FindByEmailAsync(userPaymentMethodDto.UserIdOrEmail);
                if (user == null)
                {
                    return new ApiResponse<UserPaymentMethod>
                    {
                        IsSuccess = false,
                        Message = "User not found",
                        StatusCode = 400
                    };
                }
                UserPaymentMethod userPaymentMethod = new UserPaymentMethod
                {
                    Id = new Guid(userPaymentMethodDto.Id),
                    AccountNumber = userPaymentMethodDto.AccountNumber,
                    ExpiryDate = userPaymentMethodDto.ExpiryDate,
                    IsDefault = userPaymentMethodDto.IsDefault,
                    PaymentTypeId = new Guid(userPaymentMethodDto.PaymentTypeId),
                    Provider = userPaymentMethodDto.Provider,
                    UserId = user.Id
                };
                var updatedUserPaymentMethod = await _userPaymentMethodRepository
                    .UpdateUserPaymentMethodAsync(userPaymentMethod);
                return new ApiResponse<UserPaymentMethod>
                {
                    IsSuccess = true,
                    Message = "User payment method updated successfully",
                    StatusCode = 200,
                    ResponseObject = updatedUserPaymentMethod
                };
            }
        }
    }
}
