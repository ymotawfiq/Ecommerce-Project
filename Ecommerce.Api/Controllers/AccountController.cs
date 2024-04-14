using Ecommerce.Data.DTOs.Authentication.Login;
using Ecommerce.Data.DTOs.Authentication.Register;
using Ecommerce.Data.DTOs.Authentication.ResetEmail;
using Ecommerce.Data.DTOs.Authentication.ResetPassword;
using Ecommerce.Data.DTOs.Authentication.User;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities.Authentication;
using Ecommerce.Data.Models.MessageModel;
using Ecommerce.Service.Services.EmailService;
using Ecommerce.Service.Services.UserService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;



namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<SiteUser> _userManager;
        private readonly IUserManagement _userManagementService;
        private readonly IEmailService _emailService;
        private readonly SignInManager<SiteUser> _signInManager;

        public AccountController
            (
            UserManager<SiteUser> _userManager,
            IUserManagement _userManagementService,
            IEmailService _emailService,
            SignInManager<SiteUser> _signInManager
            )
        {
            this._userManager = _userManager;
            this._userManagementService = _userManagementService;
            this._emailService = _emailService;
            this._signInManager = _signInManager;
        }


        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserDto registerUserDto)
        {

            try
            {
                var tokenResponse = await _userManagementService.CreateUserWithTokenAsync(registerUserDto);
                if (tokenResponse.ResponseObject == null || registerUserDto.Roles == null)
                {
                    throw new NullReferenceException();
                }
                if (tokenResponse.IsSuccess && tokenResponse.ResponseObject != null)
                {
                    await _userManagementService.AssignRoleToUserAsync(registerUserDto.Roles
                        , tokenResponse.ResponseObject.User);
                    var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account"
                        , new { tokenResponse.ResponseObject.Token, email = registerUserDto.Email }
                        , Request.Scheme);
                    var message = new Message(new string[] { registerUserDto.Email }
                    , "Confirmation email link", confirmationLink!);
                    var responseMsg = _emailService.SendEmail(message);

                    return StatusCode(StatusCodes.Status200OK, new ApiResponse<RegisterUserDto>
                    {
                        IsSuccess = true,
                        StatusCode = 200,
                        Message = $"Email verification link sent to ({registerUserDto.Email}) please check your inbox to verify your account"
                    });
                }
                return StatusCode(StatusCodes.Status500InternalServerError,
                  new ApiResponse<RegisterUserDto>
                  {
                      Message = "Error",
                      IsSuccess = false,
                      StatusCode = 500
                  });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<RegisterUserDto>
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Message = ex.Message
                });
            }
        }

        [HttpGet("testemailsendingmessage")]
        public  IActionResult TestEmail()
        {
            var message = new Message(new string[] { "amoyoussef2003@gmail.com" }, "Testing", "Test smtp");
            _emailService.SendEmail(message);
            return StatusCode(StatusCodes.Status200OK, "Email Sent Successfully");
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            try
            {
                var response = await _userManagementService.ConfirmEmailAsync(token, email);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<string>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Can't confirm email"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Message = ex.Message
                });
            }
        }


        [HttpPost("resendconfirmationemail")]
        public async Task<IActionResult> ResendConfirmationEmail(string email)
        {
            try
            {
                var token = await _userManagementService.ReSendEmailConfirmation(email);
                if (token.IsSuccess)
                {
                    if (token.ResponseObject != null)
                    {
                        var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account"
                                , new { token = token.ResponseObject, email = email }
                                , Request.Scheme);
                        var message = new Message(new string[] { email }
                        , "Confirmation email link", confirmationLink!);
                        var responseMsg = _emailService.SendEmail(message);
                        return StatusCode(StatusCodes.Status200OK, new ApiResponse<string>
                        {
                            StatusCode = 200,
                            IsSuccess = true,
                            Message = "Confirmation email sent successfully to your email check your inbox"
                        });
                    }
                }
                return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<string>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Can't send confirmation email"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Message = ex.Message
                });
            }

        }


        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUserDto loginUserDto)
        {
            try
            {
                var loginOtpResponse = await _userManagementService.GetOtpByLoginAsync(loginUserDto);
                if (loginOtpResponse.ResponseObject != null)
                {
                    var user = loginOtpResponse.ResponseObject.User;
                    if (user.TwoFactorEnabled)
                    {
                        var token = loginOtpResponse.ResponseObject.Token;
                        var message = new Message(new string[] { user.Email! }, "OTP Confrimation", token);
                        _emailService.SendEmail(message);

                        return StatusCode(StatusCodes.Status200OK,
                         new ApiResponse<LoginUserDto> 
                         { 
                             IsSuccess = loginOtpResponse.IsSuccess,
                             StatusCode = 200,
                             Message = $"We have sent an OTP to your Email {user.Email}" 
                         });
                    }
                    if(user!=null && await _userManager.CheckPasswordAsync(user, loginUserDto.Password))
                    {
                        var serviceResponse = await _userManagementService.GetJwtTokenAsync(user);
                        return Ok(serviceResponse);
                    }
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<LoginUserDto>
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Message = ex.Message
                });
            }
        }


        [HttpPost("login-2FA")]
        public async Task<IActionResult> LoginWithTwoFactorAuthenticationAsync(string otpCode, string email)
        {
            try
            {
                var jwt = await _userManagementService.LoginUserWithJWTokenAsync(otpCode, email);
                if (jwt.IsSuccess)
                {
                    return Ok(jwt);
                }
                return StatusCode(StatusCodes.Status404NotFound, new ApiResponse<LoginUserDto>
                {
                    IsSuccess = false,
                    StatusCode = 404,
                    Message = "Invalid OTP"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Message = ex.Message
                });
            }
        }


        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshTokenAsync(LoginResponse tokens)
        {
            try
            {
                var jwt = await _userManagementService.RenewAccessTokenAsync(tokens);
                if (jwt.IsSuccess)
                {
                    return Ok(jwt);
                }
                return StatusCode(StatusCodes.Status404NotFound, new ApiResponse<LoginUserDto>
                {
                    IsSuccess = false,
                    StatusCode = 404,
                    Message = "Invalid OTP"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Message = ex.Message
                });
            }
        }


        [HttpGet("logout")]
        public async Task LogoutAsync()
        {
            await HttpContext.SignOutAsync();
        }

        [HttpPost("forgetpassword")]
        public async Task<IActionResult> ForgetPasswordAsync(string email)
        {
            try
            {
                var response = await _userManagementService.GenerateResetPasswordTokenAsync(email);
                if (response.IsSuccess)
                {
                    if (response.ResponseObject != null)
                    {
                        var forgetPasswordLink = Url.Action(nameof(GenerateResetPasswordObject), "Account",
                        new { email = response.ResponseObject.Email, token = response.ResponseObject.Token }
                        , Request.Scheme);


                        var message = new Message(new string[] { response.ResponseObject.Email! }
                        , "Forget password link", forgetPasswordLink);
                        _emailService.SendEmail(message);
                        return StatusCode(StatusCodes.Status200OK, new ApiResponse<string>
                        {
                            StatusCode = 200,
                            IsSuccess = true,
                            Message = "Forget password sent ssuccessfully to your email"
                        });
                    }
                }

                return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<string>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Can't send forget password link to email please try again"
                });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Message = ex.Message
                });
            }
        }


        [HttpPost("resendforgetpasswordemail")]
        public async Task<IActionResult> ResendForgetPasswordEmailAsync(string email)
        {
            try
            {
                var response = await _userManagementService.GenerateResetPasswordTokenAsync(email);
                if (response.IsSuccess)
                {
                    if (response.ResponseObject != null)
                    {
                        var forgetPasswordLink = Url.Action(nameof(GenerateResetPasswordObject), "Account",
                        new { email = response.ResponseObject.Email, token = response.ResponseObject.Token }
                        , Request.Scheme);


                        var message = new Message(new string[] { response.ResponseObject.Email! }
                        , "Forget password link", forgetPasswordLink);
                        _emailService.SendEmail(message);
                        return StatusCode(StatusCodes.Status200OK, new ApiResponse<string>
                        {
                            StatusCode = 200,
                            IsSuccess = true,
                            Message = "Forget password resent ssuccessfully to your email"
                        });
                    }
                }

                return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<string>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Can't resend forget password link to email please try again"
                });
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>
                {
                    StatusCode = 500,
                    IsSuccess = false,
                    Message = ex.Message
                });
            }
        }

        [HttpGet("generateresetpasswordobject")]
        public async Task<IActionResult> GenerateResetPasswordObject(string email, string token)
        {
            try
            {
                var resetPassword = new ResetPasswordDto
                {
                    Token = token,
                    Email = email
                };
                return StatusCode(StatusCodes.Status200OK, new ApiResponse<ResetPasswordDto>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Reset password object created",
                    ResponseObject = resetPassword
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Message = ex.Message
                });
            }
        }

        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            try
            {
                var response = await _userManagementService.ResetPasswordAsync(resetPasswordDto);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }

                return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<string>
                {
                    IsSuccess = false,
                    StatusCode = 400,
                    Message = "Failed to reset password"
                });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Message = ex.Message
                });
            }
        }

        [HttpPost("sendemailtoresetemail")]
        public async Task<IActionResult> SendEmailToResetEmailAsync(string oldEmail, string newEmail)
        {
            try
            {
                var response = await _userManagementService.GenerateResetEmailTokenAsync(
                new ResetEmailDto
                {
                    OldEmail = oldEmail,
                    NewEmail = newEmail
                });
                if (response.IsSuccess)
                {
                    if (response.ResponseObject != null)
                    {
                        var emailResetLink = Url.Action(nameof(GenerateEmailResetObject), "Account",
                        new { OldEmail = oldEmail, NewEmail = newEmail, token = response.ResponseObject.Token }
                        , Request.Scheme);


                        var message = new Message(new string[] { response.ResponseObject.OldEmail! }
                        , "Email reser link", emailResetLink);
                        _emailService.SendEmail(message);
                        return StatusCode(StatusCodes.Status200OK, new ApiResponse<string>
                        {
                            StatusCode = 200,
                            IsSuccess = true,
                            Message = "Email rest link sent to your email"
                        });
                    }
                }
                return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<string>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Can't send reset email link" 
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Message = ex.Message
                });
            }
        }

        [HttpGet("generateemailresetobject")]
        public async Task<IActionResult> GenerateEmailResetObject(string OldEmail, string NewEmail, string token)
        {
            var resetEmailObject = new ResetEmailDto
            {
                NewEmail = NewEmail,
                Token = token,
                OldEmail = OldEmail
            };
            return StatusCode(StatusCodes.Status200OK, new ApiResponse<ResetEmailDto>
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Reset email object created",
                ResponseObject = resetEmailObject
            });
        }

        [HttpPost("resetemail")]
        public async Task<IActionResult> ResetEmailAsync([FromBody] ResetEmailDto resetEmail)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(resetEmail.OldEmail);
                if (user != null)
                {
                    await _userManager.ChangeEmailAsync(user, resetEmail.NewEmail, resetEmail.Token);
                    user.UserName = resetEmail.NewEmail;
                    await _userManager.UpdateAsync(user);
                    return StatusCode(StatusCodes.Status200OK, new ApiResponse<string>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = "Email changed successfully"
                    });
                }
                return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<string>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Unable to reset email"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Message = ex.Message
                });
            }
        }

        [HttpGet("getcurrentuser")]
        public ClaimsPrincipal GetCurrentUserAsync()
        {
            return HttpContext.User;
        }

    }
}
