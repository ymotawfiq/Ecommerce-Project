

using Ecommerce.Data.DTOs.Authentication.Login;
using Ecommerce.Data.DTOs.Authentication.Register;
using Ecommerce.Data.DTOs.Authentication.User;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities.Authentication;

namespace Ecommerce.Service.Services.UserService
{
    public interface IUserManagement
    {
        Task<ApiResponse<CreateUserResponse>> CreateUserWithTokenAsync(RegisterUserDto registerUser);
        Task<ApiResponse<List<string>>> AssignRoleToUserAsync(List<string> roles, SiteUser user);
        Task<ApiResponse<LoginOtpResponse>> GetOtpByLoginAsync(LoginUserDto loginModel);
        Task<ApiResponse<LoginResponse>> GetJwtTokenAsync(SiteUser user);
        Task<ApiResponse<LoginResponse>> LoginUserWithJWTokenAsync(string otp, string email);
        Task<ApiResponse<LoginResponse>> RenewAccessTokenAsync(LoginResponse tokens);

    }
}
