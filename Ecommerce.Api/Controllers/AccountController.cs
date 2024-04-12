using Ecommerce.Data.DTOs.Authentication.Login;
using Ecommerce.Data.DTOs.Authentication.Register;
using Ecommerce.Data.DTOs.Authentication.User;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities.Authentication;
using Ecommerce.Data.Models.MessageModel;
using Ecommerce.Service.Services.EmailService;
using Ecommerce.Service.Services.UserService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<SiteUser> _userManager;
        private readonly IUserManagement _userManagementService;
        private readonly IEmailService _emailService;

        public AccountController(UserManager<SiteUser> _userManager, IUserManagement _userManagementService,
            IEmailService _emailService)
        {
            this._userManager = _userManager;
            this._userManagementService = _userManagementService;
            this._emailService = _emailService;
        }

        [HttpPost("Register")]
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
                        Message = $"{tokenResponse.Message} {responseMsg}"
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
                var user = await _userManager.FindByEmailAsync(email);
                if (user != null)
                {
                    var result = await _userManager.ConfirmEmailAsync(user, token);
                    if (result.Succeeded)
                    {
                        return StatusCode(StatusCodes.Status200OK, new ApiResponse<SiteUser>
                        {
                            IsSuccess = true,
                            Message = "Email verified successfully",
                            StatusCode = 200,
                        });
                    }
                }
                return StatusCode(StatusCodes.Status404NotFound, new ApiResponse<SiteUser>
                {
                    IsSuccess = false,
                    Message = "User not exists",
                    StatusCode = 404,
                    ResponseObject = user
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


    }
}
