

using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Data.DTOs.Authentication.ResetPassword
{
    public class ResetPasswordDto
    {
        [Required]
        public string Password { get; set; } = string.Empty;
        [Compare("Password", ErrorMessage = "The password and conformation password don't match...")]
        public string ConfirmPassword { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
