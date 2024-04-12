
using Ecommerce.Data.DTOs;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Service.Services.UserAddressService
{
    public interface IUserAddressService
    {
        Task<ApiResponse<IEnumerable<UserAddress>>> GetAllUserAddressAsync();
        Task<ApiResponse<IEnumerable<UserAddress>>> GetUserAddressesByUserIdAsync(Guid userId);
        Task<ApiResponse<IEnumerable<UserAddress>>> GetUserAddressesByUsernameOrEmailAsync
            (string userNameOrEmail);
        Task<ApiResponse<UserAddress>> AddUserAddressAsync(UserAddressDto userAddressDto);
        Task<ApiResponse<UserAddress>> UpdateUserAddressAsync(UserAddressDto userAddressDto);
        Task<ApiResponse<UserAddress>> GetUserAddressByIdAsync(Guid userAddressId);
        Task<ApiResponse<UserAddress>> DeleteUserAddressByIdAsync(Guid userAddressId);
    }
}
