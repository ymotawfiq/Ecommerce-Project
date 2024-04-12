

using Azure.Core;
using Ecommerce.Data.DTOs.Authentication.Login;
using Ecommerce.Data.DTOs.Authentication.Register;
using Ecommerce.Data.DTOs.Authentication.ResetEmail;
using Ecommerce.Data.DTOs.Authentication.ResetPassword;
using Ecommerce.Data.DTOs.Authentication.User;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities.Authentication;
using Ecommerce.Data.Models.MessageModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;

namespace Ecommerce.Service.Services.UserService
{
    public class UserManagement : IUserManagement
    {
        private readonly UserManager<SiteUser> _userManager;
        private readonly SignInManager<SiteUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public UserManagement(UserManager<SiteUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<SiteUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _configuration = configuration;

        }

        public async Task<ApiResponse<string>> ConfirmEmailAsync(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    return new ApiResponse<string>
                    {
                        IsSuccess = true,
                        Message = "Email verified successfully",
                        StatusCode = 200,
                    };
                }
            }
            return new ApiResponse<string>
            {
                IsSuccess = false,
                Message = "User not exists",
                StatusCode = 404,
            };
        }

        public async Task<ApiResponse<List<string>>> AssignRoleToUserAsync(List<string> roles, SiteUser user)
        {
            var assignrole = new List<string>();
            foreach(var role in roles)
            {
                if(await _roleManager.RoleExistsAsync(role))
                {
                    if(!await _userManager.IsInRoleAsync(user, role))
                    {
                        await _userManager.AddToRoleAsync(user, role);
                        assignrole.Add(role);
                    }
                }
            }
            return new ApiResponse<List<string>>
            {
                IsSuccess = true,
                Message = "Roles assigned successfully",
                StatusCode = 200,
                ResponseObject = assignrole
            };
        }

        public async Task<ApiResponse<string>> ReSendEmailConfirmation(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new ApiResponse<string>
                {
                    IsSuccess = false,
                    Message = "User doesn't exist",
                    StatusCode = 400
                };
            }
            if (user.EmailConfirmed)
            {
                return new ApiResponse<string>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Email already confirmed"
                };
            }
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            return new ApiResponse<string>
            {
                ResponseObject = token
            };
        }

        public async Task<ApiResponse<CreateUserResponse>> CreateUserWithTokenAsync(RegisterUserDto registerUser)
        {
            var userExist = await _userManager.FindByEmailAsync(registerUser.Email);
            if (userExist != null)
            {
                return new ApiResponse<CreateUserResponse>
                {
                    IsSuccess = false,
                    Message = "User already exists",
                    StatusCode = 403,
                    ResponseObject = new()
                };
            }
            SiteUser user = new SiteUser
            {
                FirstName = registerUser.FirstName,
                LastName = registerUser.LastName,
                UserName = registerUser.Email,
                Email = registerUser.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                TwoFactorEnabled = true
            };
            var result = await _userManager.CreateAsync(user, registerUser.Password);
            if (result.Succeeded)
            {
                await _userManager.SetTwoFactorEnabledAsync(user, true);
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                return new ApiResponse<CreateUserResponse>
                {
                    IsSuccess = true,
                    StatusCode = 201,
                    Message = "User created successfully",
                    ResponseObject = new CreateUserResponse
                    {
                        User = user,
                        Token = token
                    }
                };
            }
            else
            {
                return new ApiResponse<CreateUserResponse>
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Message = "Failed to create user",
                    ResponseObject = new()
                };
            }
        }

        public async Task<ApiResponse<LoginResponse>> GetJwtTokenAsync(SiteUser user)
        {
            if(user.UserName == null)
            {
                throw new NullReferenceException("Username must not be null");
            }
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach(var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            var jwtToken = GetToken(authClaims);
            var refreshToken = GenerateRefreshToken();
            _ = int.TryParse(_configuration["JWT:RefreshTokenValidity"], out int refreshTokenValidity);

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpierationDate = DateTime.UtcNow.AddDays(refreshTokenValidity);
            await _userManager.UpdateAsync(user);
            return new ApiResponse<LoginResponse>
            {
                IsSuccess = true,
                Message = "Token created",
                StatusCode = 200,
                ResponseObject = new LoginResponse
                {
                    AccessToken = new TokenType()
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                        ExpiryTokenDate = jwtToken.ValidTo
                    },
                    RefreshToken = new TokenType()
                    {
                        Token = user.RefreshToken,
                        ExpiryTokenDate = (DateTime)user.RefreshTokenExpierationDate
                    }
                }
            };
        }

        public async Task<ApiResponse<LoginOtpResponse>> GetOtpByLoginAsync(LoginUserDto loginModel)
        {
            var user = await _userManager.FindByEmailAsync(loginModel.Email);
            if (user != null)
            {
                await _signInManager.SignOutAsync();
                await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, true);
                if (user.TwoFactorEnabled)
                {
                    if (!user.EmailConfirmed)
                    {
                        return new ApiResponse<LoginOtpResponse>
                        {
                            IsSuccess = false,
                            StatusCode = 400,
                            Message = "Please confirm your email"
                        };
                    }
                    var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");
                    return new ApiResponse<LoginOtpResponse>
                    {
                        IsSuccess = true,
                        StatusCode = 200,
                        Message = "OTP sent to your email",
                        ResponseObject = new LoginOtpResponse
                        {
                            IsTwoFactorEnable = user.TwoFactorEnabled,
                            User = user,
                            Token = token
                        }
                    };
                }
                else
                {
                    return new ApiResponse<LoginOtpResponse>
                    {
                        IsSuccess = true,
                        Message = "Two factor authentication not enabled",
                        StatusCode = 200,
                        ResponseObject = new LoginOtpResponse
                        {
                            IsTwoFactorEnable = user.TwoFactorEnabled,
                            Token = string.Empty,
                            User = user
                        }
                    };
                }
            }

            return new ApiResponse<LoginOtpResponse>
            {
                IsSuccess = false,
                StatusCode = 404,
                Message = $"User doesnot exist."
            };

        }

        public async Task<ApiResponse<LoginResponse>> LoginUserWithJWTokenAsync(string otp, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var signIn = await _signInManager.TwoFactorSignInAsync("Email", otp, false, false);
            if (signIn.Succeeded)
            {
                if (user != null)
                {
                    return await GetJwtTokenAsync(user);
                }
            }
            return new ApiResponse<LoginResponse>
            {
                ResponseObject = new LoginResponse()
                {

                },
                IsSuccess = false,
                StatusCode = 400,
                Message = $"Invalid Otp"
            };
        }

        public async Task<ApiResponse<LoginResponse>> RenewAccessTokenAsync(LoginResponse tokens)
        {
            var accessToken = tokens.AccessToken;
            var refreshToken = tokens.RefreshToken;
            if(accessToken == null || refreshToken == null || refreshToken.Token==null )
            {
                throw new NullReferenceException();
            }
            var principal = GetClaimsPrincipal(accessToken.Token);
            var user = await _userManager.FindByNameAsync(principal.Identity.Name);
            if (refreshToken.Token != user.RefreshToken && refreshToken.ExpiryTokenDate <= DateTime.Now)
            {
                return new ApiResponse<LoginResponse>
                {

                    IsSuccess = false,
                    StatusCode = 400,
                    Message = $"Token invalid or expired"
                };
            }
            var response = await GetJwtTokenAsync(user);
            return response;
        }

        public async Task<ApiResponse<ResetPasswordDto>> GenerateResetPasswordTokenAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                return new ApiResponse<ResetPasswordDto>
                {
                    IsSuccess= true,
                    StatusCode = 200,
                    Message = "Reset password token generated successfully",
                    ResponseObject = new ResetPasswordDto
                    {
                        Email = email,
                        Token = token
                    }
                };
            }
            return new ApiResponse<ResetPasswordDto>
            {
                StatusCode = 400,
                IsSuccess = false,
                Message = "Can't generate reset password token"
            };
        }

        public async Task<ApiResponse<string>> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user != null)
            {
                var resetPassword = await _userManager
                    .ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);
                if (!resetPassword.Succeeded)
                {
                    return new ApiResponse<string>
                    {
                        IsSuccess = false,
                        StatusCode = 400,
                        Message = "Failed to reset password",
                        ResponseObject = resetPassword.Errors.ToString()
                    };
                }

                return new ApiResponse<string>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Password reset successfully"
                };
            }
            return new ApiResponse<string>
            {
                StatusCode = 400,
                IsSuccess = false,
                Message = "Can't send link to email please try again"
            };
        }

        public async Task<ApiResponse<ResetEmailDto>> GenerateResetEmailTokenAsync(ResetEmailDto resetEmail)
        {
            var existingUser = await _userManager.FindByEmailAsync(resetEmail.NewEmail);
            if (existingUser != null)
            {
                return new ApiResponse<ResetEmailDto>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Email already exists please provide another email"
                };
            }
            var user = await _userManager.FindByEmailAsync(resetEmail.OldEmail);
            if (user != null)
            {
                var token = await _userManager.GenerateChangeEmailTokenAsync(user, resetEmail.NewEmail);
                resetEmail.Token = token;
                return new ApiResponse<ResetEmailDto>
                {
                    IsSuccess = true,
                    Message = "Reset email token generated successfully",
                    StatusCode = 200,
                    ResponseObject = resetEmail
                };
            }
            return new ApiResponse<ResetEmailDto>
            {
                IsSuccess = true,
                Message = "Can't generate reset email token",
                StatusCode = 400,
                ResponseObject = null
            };
        }

        #region PrivateMethods
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);
            var expirationTimeUtc = DateTime.UtcNow.AddMinutes(tokenValidityInMinutes);
            var localTimeZone = TimeZoneInfo.Local;
            var expirationTimeInLocalTimeZone = TimeZoneInfo.ConvertTimeFromUtc(expirationTimeUtc, localTimeZone);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: expirationTimeInLocalTimeZone,
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new Byte[64];
            var range = RandomNumberGenerator.Create();
            range.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private ClaimsPrincipal GetClaimsPrincipal(string accessToken)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out SecurityToken securityToken);

            return principal;

        }

        #endregion



    }
}
