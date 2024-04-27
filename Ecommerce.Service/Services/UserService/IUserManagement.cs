

using Ecommerce.Data.DTOs.Authentication.EmailConfirmation;
using Ecommerce.Data.DTOs.Authentication.Login;
using Ecommerce.Data.DTOs.Authentication.Register;
using Ecommerce.Data.DTOs.Authentication.ResetEmail;
using Ecommerce.Data.DTOs.Authentication.ResetPassword;
using Ecommerce.Data.DTOs.Authentication.User;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities.Authentication;

namespace Ecommerce.Service.Services.UserService
{
    public interface IUserManagement
    {
        Task<ApiResponse<CreateUserResponse>> CreateUserWithTokenAsync(RegisterUserDto registerDto);
        Task<ApiResponse<List<string>>> AssignRolesToUserAsync(List<string> roles, SiteUser user);
        Task<ApiResponse<LoginOtpResponse>> LoginUserAsync(LoginUserDto loginDto);
        Task<ApiResponse<LoginResponse>> GetJwtTokenAsync(SiteUser user);
        Task<ApiResponse<LoginResponse>> LoginUserWithOTPAsync(string otp, string userNameOrEmail);
        Task<ApiResponse<LoginResponse>> RenewAccessTokenAsync(LoginResponse tokens);
        Task<ApiResponse<string>> ConfirmEmail(string userNameOrEmail, string token);
        Task<ApiResponse<string>> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
        Task<ApiResponse<ResetPasswordDto>> GenerateResetPasswordTokenAsync(string email);
        Task<ApiResponse<EmailConfirmationDto>> GenerateEmailConfirmationTokenAsync(string email);
        Task<ApiResponse<ResetEmailDto>> GenerateResetEmailTokenAsync(ResetEmailObjectDto resetEmailObjectDto);
        Task<ApiResponse<string>> EnableTwoFactorAuthenticationAsync(string email);
        Task<ApiResponse<string>> DeleteAccountAsync(string userNameOrEmail);

    }
}
